using Amazon.DynamoDBv2.DataModel;
using BPOR.Domain.Entities;
using BPOR.Domain.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DynamoDBupdate.Backfills
{
    public class Stage2Backfill
    {
        private readonly ParticipantDbContext _participantDbContext;
        private readonly ILogger<Stage2Backfill> _logger;
        private readonly DynamoDBOperationConfig _config;
        private readonly IDynamoDBContext _dynamoContext;

        public Stage2Backfill(ParticipantDbContext participantDbContext, ILogger<Stage2Backfill> logger, IDynamoDBContext participantRepository, DynamoDBOperationConfig config)
        {
            _participantDbContext = participantDbContext;
            _logger = logger;
            _dynamoContext = participantRepository;
            _config = config;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var participantsToBeUpdated = await _participantDbContext.Participants
                .Where(x =>
                    x.Stage2CompleteUtc == null &&
                    x.FirstName != null &&
                    x.LastName != null &&
                    !x.IsDeleted &&
                    x.RegistrationConsent == true &&
                    (
                        x.Address.Postcode != null ||
                        x.EthnicBackground != null ||
                        x.EthnicGroup != null ||
                        x.HasLongTermCondition != null ||
                        x.GenderId != null
                    )
                )
                .Include(x => x.SourceReferences)
                .Select(x => new
                {
                    Id = x.Id,
                    ParticipantIdentifiers = x.SourceReferences.Select(y => y.Pk),
                    CreatedAt = x.CreatedAt
                })
                .Take(1) // // TODO: remove after testing
                .ToListAsync(cancellationToken);

            int totalRecords = participantsToBeUpdated.Count;
            int currentRecordNum = 1;
            int recordsInError = 0;
            _logger.LogInformation("Total number of accounts to be updated: {Count}", participantsToBeUpdated.Count);

            foreach (var toBeUpdated in participantsToBeUpdated)
            {
                if (toBeUpdated.ParticipantIdentifiers == null)
                {
                    _logger.LogError($"could not find identifiers for participant @{toBeUpdated.Id}");
                    continue;
                }
                try
                {
                    _logger.LogInformation($"{currentRecordNum}/{totalRecords} updating participant {toBeUpdated.Id}, setting stage2CompleteUtc to {toBeUpdated.CreatedAt}");

                    DynamoParticipant participant = null;
                    // participant should only have 1 active record
                    foreach (var participantIdentifier in toBeUpdated.ParticipantIdentifiers)
                    {
                        participant = await _dynamoContext.LoadAsync<DynamoParticipant>(participantIdentifier, _config, cancellationToken);
                        if (participant != null)
                        {
                            break;
                        }
                    }

                    if (participant == null)
                    {
                        // test data may not have a value equivilent value in dynamodb
                        _logger.LogError($"participant {toBeUpdated.Id}, not found in dynamodb");
                        continue;
                    }
                    participant.IsStage2CompleteUtcBackfilled = true;
                    participant.Stage2CompleteUtc = participant.CreatedAtUtc;
                    await _dynamoContext.SaveAsync(participant, _config, cancellationToken);
                    currentRecordNum++;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error updating participant {ParticipantId}", toBeUpdated.Id);

                    currentRecordNum++;
                    recordsInError++;

                }
            }

            // sleep for a minute to allow the stream handler time to settle
            Thread.Sleep(60000);

            // report back on results
            var participantsUpdated = await _participantDbContext.Participants.Where(x => x.Stage2CompleteUtc == null
                && x.FirstName != null
                && x.LastName != null
                && x.IsDeleted == false
                && x.RegistrationConsent == true
                && (
                    x.Address.Postcode != null
                    || x.EthnicBackground != null
                    || x.EthnicGroup != null
                    || x.HasLongTermCondition != null
                    || x.GenderId != null
                )
            ).Include(x => x.SourceReferences).Select(x => new
            {
                Id = x.Id
            }
            ).ToListAsync(cancellationToken);

            _logger.LogInformation($"number of accounts in error {recordsInError}");

            if (participantsUpdated.Count > 0)
            {
                _logger.LogInformation($"accounts not updated: {participantsUpdated.Count}");
                _logger.LogInformation($"Ids of accounts not updated {String.Join(",", participantsUpdated)}");
            }

            if (participantsUpdated.Count == 0)
            {
                _logger.LogInformation($"accounts updated successfully");
            }
        }
        
        public async Task RollBack(CancellationToken cancellationToken)
        {
            var participantsToBeUpdated = await  _participantDbContext.Participants.Where(x => x.IsStage2CompleteUtcBackfilled == true
              ).Include(x => x.ParticipantIdentifiers).Select(x => new
              {
                  Id = x.Id,
                  ParticipantIdentifiers = x.SourceReferences.Select(y => y.Pk),
                  CreatedAt = x.CreatedAt
              }
              ).ToListAsync(cancellationToken);

            int totalRecords = participantsToBeUpdated.Count;
            int currentRecordNum = 1;
            int recordsInError = 0;

            foreach (var toBeUpdated in participantsToBeUpdated)
            {
                if (toBeUpdated.ParticipantIdentifiers == null)
                {
                    _logger.LogError($"could not find identifiers for participant @{toBeUpdated.Id}");
                    continue;
                }
                try
                {
                    _logger.LogInformation($"{currentRecordNum}/{totalRecords} updating participant {toBeUpdated.Id}, setting stage2CompleteUtc to null");

                    DynamoParticipant participant = null;
                    // participant should only have 1 active record
                    foreach (var participantIdentifier in toBeUpdated.ParticipantIdentifiers)
                    {
                        participant = await _dynamoContext.LoadAsync<DynamoParticipant>(participantIdentifier, _config, cancellationToken);
                        if (participant != null)
                        {
                            break;
                        }
                    }

                    if (participant == null)
                    {
                        // test data may not have a value equivilent value in dynamodb
                        _logger.LogError("ParticipantId {ParticipantId} not found in DynamoDB", toBeUpdated.Id);

                        continue;
                    }
                    participant.IsStage2CompleteUtcBackfilled = false;
                    participant.Stage2CompleteUtc = null;
                    await _dynamoContext.SaveAsync(participant, _config, cancellationToken);
                    currentRecordNum++;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error updating participant {ParticipantId}", toBeUpdated.Id);
                    currentRecordNum++;
                    recordsInError++;
                }
            }

            // sleep for a minute to allow the stream handler time to settle
            Thread.Sleep(60000);
            // report back on results.
            var participantsUpdated = await _participantDbContext.Participants.Where(x => x.IsStage2CompleteUtcBackfilled == true
              ).Include(x => x.ParticipantIdentifiers).Select(x => new
              {
                  Id = x.Id,
              }
              ).ToListAsync(cancellationToken);

            _logger.LogInformation($"number of accounts in error {recordsInError}");

            if (participantsUpdated.Count > 0)
            {
                _logger.LogInformation($"accounts not updated: {participantsUpdated.Count}");
                _logger.LogInformation("Ids of accounts not updated: {Ids}", string.Join(",", participantsUpdated));
            }

            if (participantsUpdated.Count == 0)
            {
                _logger.LogInformation($"accounts updated successfully");
            }
        }
    }
}