using Amazon.DynamoDBv2.DataModel;
using BPOR.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace DynamoDBupdate.Backfills
{
    public class Stage2BackfillUpdate
    {
        private readonly ILogger<Stage2Backfill> _logger;
        private readonly IDynamoDBContext _dynamoContext;
        private readonly DynamoDBOperationConfig _dynamoDBOperationConfig;
        private readonly IOptions<OsSettings> _osSettings;

        public Stage2BackfillUpdate(
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
            if (!_osSettings.Value.RunStage2BackfillUpdate)
            {
                _logger.LogInformation("Not running {job}", nameof(Stage2BackfillUpdate));
                return;
            }

            var scanConditions = new List<ScanCondition>();
            var scan = _dynamoContext.ScanAsync<DynamoParticipant>(scanConditions, _dynamoDBOperationConfig);
            var sw = new Stopwatch();
            var swTotal = new Stopwatch();
            sw.Start();
            swTotal.Start();
            var total = 0;

            var file = File.OpenWrite("UpdateSQL.sql");
            using var swr = new StreamWriter(file);
            while (!scan.IsDone)
            {
                var set = await scan.GetNextSetAsync(cancellationToken);

                _logger.LogInformation("Loaded set of {itemCount} items in {ms}ms.", set.Count, sw.ElapsedMilliseconds);
                sw.Restart();
                foreach (var p in set)
                {
                    if (p.IsStage2CompleteUtcBackfilled.GetValueOrDefault())
                    {
                        swr.WriteLine($"UPDATE Participants p LEFT JOIN SourceReference s ON p.id = s.participantid SET p.stage2completeutc='{p.Stage2CompleteUtc?.ToString("yyyy-MM-dd hh:mm:ss")}', p.IsStage2CompleteUtcBackfilled=true, p.updatedat='{DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss")}' WHERE s.Pk = '{p.Pk}';");
                    }
                    total++;
                }
            }

            swr.Flush();
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