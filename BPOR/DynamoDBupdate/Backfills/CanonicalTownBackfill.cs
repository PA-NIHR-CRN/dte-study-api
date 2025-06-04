using Amazon.DynamoDBv2.DataModel;
using BPOR.Domain.Entities;
using BPOR.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NIHR.Infrastructure;

namespace DynamoDBupdate.Backfills
{
    public class CanonicalTownBackfill
    {
        private readonly ParticipantDbContext _participantDbContext;
        private readonly ILogger<CanonicalTownBackfill> _logger;
        private readonly DynamoDBOperationConfig _config;
        private readonly IDynamoDBContext _dynamoContext;
        private readonly IPostcodeMapper _postcodeMapper;

        public CanonicalTownBackfill(
            ParticipantDbContext participantDbContext,
            ILogger<CanonicalTownBackfill> logger,
            IDynamoDBContext participantRepository,
            DynamoDBOperationConfig config,
            IPostcodeMapper postcodeMapper)
        {
            _participantDbContext = participantDbContext;
            _logger = logger;
            _dynamoContext = participantRepository;
            _config = config;
            _postcodeMapper = postcodeMapper;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var participantsToBeUpdated = await _participantDbContext.Participants
                .Where(x => x.CanonicalTown == null && x.Address.Postcode != null && !x.IsDeleted)
                .Include(x => x.SourceReferences)
                .Select(x => new
                {
                    Id = x.Id,
                    ParticipantIdentifiers = x.SourceReferences.Select(y => y.Pk),
                    Postcode = x.Address.Postcode
                })
                .ToListAsync(cancellationToken);

            int totalRecords = participantsToBeUpdated.Count;
            int currentRecordNum = 1;
            int recordsInError = 0;
            _logger.LogInformation("Total number of accounts to be updated with canonical town: {Count}", totalRecords);

            foreach (var toBeUpdated in participantsToBeUpdated)
            {
                if (toBeUpdated.ParticipantIdentifiers == null || !toBeUpdated.ParticipantIdentifiers.Any())
                {
                    _logger.LogError("Could not find identifiers for participant @{ParticipantId}", toBeUpdated.Id);
                    continue;
                }

                try
                {
                    _logger.LogInformation("{Current}/{Total} Updating participant {ParticipantId} with canonical town from postcode {Postcode}",
                        currentRecordNum, totalRecords, toBeUpdated.Id, toBeUpdated.Postcode);

                    var canonicalTown = await GetCanonicalTownAsync(toBeUpdated.Postcode, cancellationToken);

                    if (string.IsNullOrWhiteSpace(canonicalTown))
                    {
                        _logger.LogWarning("No canonical town found for postcode {Postcode} (participant {ParticipantId})", toBeUpdated.Postcode, toBeUpdated.Id);
                        continue;
                    }

                    DynamoParticipant participant = null;
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
                        _logger.LogError("Participant {ParticipantId} not found in DynamoDB", toBeUpdated.Id);
                        continue;
                    }

                    participant.CanonicalTown = canonicalTown;
                    participant.IsCanonicalTownBackfilled = true;

                    await _dynamoContext.SaveAsync(participant, _config, cancellationToken);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error updating participant {ParticipantId}", toBeUpdated.Id);
                    recordsInError++;
                }

                currentRecordNum++;
            }

            Thread.Sleep(60000);
            _logger.LogInformation("Number of accounts in error: {Count}", recordsInError);
        }

        public async Task RollBack(CancellationToken cancellationToken)
        {
            var participantsToUpdate = await _participantDbContext.Participants
                .Where(x => x.IsCanonicalTownBackfilled == true)
                .Include(x => x.SourceReferences)
                .Select(x => new
                {
                    Id = x.Id,
                    ParticipantIdentifiers = x.SourceReferences.Select(y => y.Pk)
                })
                .ToListAsync(cancellationToken);

            int totalRecords = participantsToUpdate.Count;
            int currentRecordNum = 1;
            int recordsInError = 0;

            foreach (var toBeUpdated in participantsToUpdate)
            {
                try
                {
                    _logger.LogInformation("{Current}/{Total} Rolling back participant {ParticipantId}", currentRecordNum, totalRecords, toBeUpdated.Id);

                    DynamoParticipant participant = null;
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
                        _logger.LogError("ParticipantId {ParticipantId} not found in DynamoDB", toBeUpdated.Id);
                        continue;
                    }

                    participant.CanonicalTown = null;
                    participant.IsCanonicalTownBackfilled = false;

                    await _dynamoContext.SaveAsync(participant, _config, cancellationToken);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error rolling back participant {ParticipantId}", toBeUpdated.Id);
                    recordsInError++;
                }

                currentRecordNum++;
            }

            Thread.Sleep(60000);
            _logger.LogInformation("Number of rollback errors: {Count}", recordsInError);
        }

        private async Task<string?> GetCanonicalTownAsync(string postcode, CancellationToken cancellationToken)
        {
            try
            {
                var addresses = await _postcodeMapper.GetAddressesByPostcodeAsync(postcode, cancellationToken);
                return addresses?.FirstOrDefault()?.Town;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving canonical town for postcode: {Postcode}", postcode);
                return null;
            }
        }
    }
}