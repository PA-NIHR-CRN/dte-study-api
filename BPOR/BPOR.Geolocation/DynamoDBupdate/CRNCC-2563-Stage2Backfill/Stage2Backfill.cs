using BPOR.Domain.Entities;
using BPOR.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DynamoDBupdate.CRNCC2563Stage2Backfill
{
    public class Stage2Backfill : IStage2Backfill
    {
        private readonly ParticipantDbContext _participantDbContext;
        private readonly ILogger<Stage2Backfill> _logger;
        private IParticipantRepository _participantRepository;

        public Stage2Backfill(ParticipantDbContext participantDbContext, ILogger<Stage2Backfill> logger, IParticipantRepository participantRepository)
        {
            _participantDbContext = participantDbContext;
            _logger = logger;
            _participantRepository = participantRepository;

            }


        public async Task RunAsync()
        {
            IEnumerable<Participant> participantsToBeUpdated = _participantDbContext.Participants.Where(x => x.Stage2CompleteUtc == null
            && x.Address.Postcode != null
            && x.EthnicBackground != null
            && x.EthnicGroup != null
            && x.FirstName != null
            && x.LastName != null
            && x.RegistrationConsent == true
            && x.HasLongTermCondition != null
            && x.GenderId != null
            && x.IsDeleted == false
            ).Include(x=>x.ParticipantIdentifiers);
            foreach (Participant TobeUpdated in participantsToBeUpdated)
            {
                if (TobeUpdated.ParticipantIdentifiers == null)
                {
                    _logger.LogError($"could not find identifiers for participant @{TobeUpdated.Id}");
                    continue;
                }
                try
                {
                    _logger.LogInformation($"updating participant {TobeUpdated.Id}, setting stage2CompleteUtc t0 {TobeUpdated.CreatedAt}");


                    DynamoParticipant participant = null;
                    // participant should only have 1 active record
                    foreach (ParticipantIdentifier participantIdentifier in TobeUpdated.ParticipantIdentifiers)
                    {
                        participant = await _participantRepository.GetParticipantAsync(participantIdentifier.Value.ToString(), new CancellationToken());
                        if (participant != null)
                        {
                            break;
                        }
                    }

                    if (participant == null)
                    {
                        // test data may not have a value equivilent value in dynamodb
                        _logger.LogError($"participant {TobeUpdated.Id}, not found in dynamodb");
                        continue;
                    }
                    participant.IsStage2CompleteUtcBackfilled = true;
                    participant.Stage2CompleteUtc = participant.CreatedAtUtc;
                    await _participantRepository.UpdateParticipantAsync(participant, new CancellationToken());
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"details of participant associated with error {TobeUpdated.Id}");

                }


            }


        }

	}
}

