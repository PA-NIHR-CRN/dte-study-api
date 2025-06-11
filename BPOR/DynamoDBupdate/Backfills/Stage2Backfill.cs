using Amazon.DynamoDBv2.DataModel;
using BPOR.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace DynamoDBupdate.Backfills
{
    public class Stage2Backfill
    {
        private readonly ILogger<Stage2Backfill> _logger;
        private readonly IDynamoDBContext _dynamoContext;
        private readonly DynamoDBOperationConfig _dynamoDBOperationConfig;
        private readonly IOptions<OsSettings> _osSettings;

        public Stage2Backfill(
            ILogger<Stage2Backfill> logger,
            IDynamoDBContext dynamoDBContext,
            DynamoDBOperationConfig dynamoDBOperationConfig,
            IOptions<OsSettings> osSettings
            )
        {
            _logger = logger;
            _dynamoContext = dynamoDBContext;
            _dynamoDBOperationConfig = dynamoDBOperationConfig;
            _osSettings = osSettings;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            if (!_osSettings.Value.RunStage2CompleteBackfill)
            {
                _logger.LogInformation("Not running {job}", nameof(Stage2Backfill));
                return;
            }

            var scanConditions = new List<ScanCondition>();
            var scan = _dynamoContext.ScanAsync<DynamoParticipant>(scanConditions, _dynamoDBOperationConfig);
            var sw = new Stopwatch();
            var swTotal = new Stopwatch();
            sw.Start();
            swTotal.Start();
            var total = 0;
            while (!scan.IsDone)
            {
                var batchWrite = _dynamoContext.CreateBatchWrite<DynamoParticipant>(_dynamoDBOperationConfig);
                var set = await scan.GetNextSetAsync(cancellationToken);

                _logger.LogInformation("Loaded set of {itemCount} items in {ms}ms.", set.Count, sw.ElapsedMilliseconds);
                sw.Restart();
                foreach (var p in set)
                {
                    if (TryBackfill(p))
                    {
                        p.IsStage2CompleteUtcBackfilled = true;
                        batchWrite.AddPutItem(p);
                    }
                }

                _logger.LogInformation("Writing set of {itemCount} items in {ms}ms.", set.Count, sw.ElapsedMilliseconds);
                sw.Restart();
                await batchWrite.ExecuteAsync(cancellationToken);
                _logger.LogInformation("Written set of {itemCount} items in {ms}ms.", set.Count, sw.ElapsedMilliseconds);
                sw.Restart();

                total += set.Count;
                _logger.LogInformation("Total written {total} in {s}s", total, (int)swTotal.ElapsedMilliseconds / 1000);
            }
        }

        private bool TryBackfill(DynamoParticipant p)
        {
            var isDeleted = p.Pk.StartsWith("DELETED#", StringComparison.InvariantCultureIgnoreCase);
            var isStage2Complete = !isDeleted
                && p.ConsentRegistrationAtUtc is not null
                && p.RemovalOfConsentRegistrationAtUtc is null
                && p.Firstname is not null
                && p.Lastname is not null
                && p.Email is not null
                && p.EthnicGroup is not null
                && p.EthnicBackground is not null
                && p.SexRegisteredAtBirth is not null
                && p.Disability is not null
                && p.Address?.Postcode is not null;

            if (isStage2Complete && p.Stage2CompleteUtc is null)
            {
                p.Stage2CompleteUtc = p.UpdatedAtUtc.HasValue ? p.UpdatedAtUtc : p.CreatedAtUtc != DateTime.MinValue ? p.CreatedAtUtc : DateTime.UtcNow;
                p.IsStage2CompleteUtcBackfilled = true;

                return true;
            }
            else if (!isStage2Complete && p.Stage2CompleteUtc is not null && !isDeleted)
            {
                _logger.LogWarning("{Pk} is marked as Stage2Complete but does not meet the criteria.", p.Pk);
            }

            return false;
        }
    }
}