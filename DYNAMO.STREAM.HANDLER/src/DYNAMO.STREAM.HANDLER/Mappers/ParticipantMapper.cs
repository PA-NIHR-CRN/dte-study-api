using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Dte.Common.Contracts;
using DYNAMO.STREAM.HANDLER.Contracts;
using DYNAMO.STREAM.HANDLER.Entities;
using DynamoParticipant = Domain.Entities.Participants.Participant;

namespace DYNAMO.STREAM.HANDLER.Mappers;

public class ParticipantMapper : IParticipantMapper
{
    private readonly IDynamoDBContext _context;
    private readonly IRefDataService _refDataService;
    private readonly IClock _clock;

    public ParticipantMapper(IDynamoDBContext context, IRefDataService refDataService, IClock clock)
    {
        _context = context;
        _refDataService = refDataService;
        _clock = clock;
    }

    private void MapIdentifiers(DynamoParticipant source, Participant participant)
    {
        // Iterate over possible identifiers (ParticipantId, NhsId, etc.)
        var identifiers = new List<(string type, string value)>
        {
            ("ParticipantId", source.ParticipantId),
            ("NhsId", source.NhsId)
        };

        foreach (var (type, value) in identifiers)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var identifierTypeId = _refDataService.GetIdentifierTypeId(type);
                if (identifierTypeId != null)
                {
                    if (!participant.ParticipantIdentifiers.Any(pi =>
                            pi.Value == value && pi.IdentifierTypeId == identifierTypeId))
                    {
                        var newIdentifier = new ParticipantIdentifier
                        {
                            ParticipantId = participant.Id,
                            Value = value,
                            IdentifierTypeId = identifierTypeId,
                            IsDeleted = false
                        };

                        participant.ParticipantIdentifiers.Add(newIdentifier);
                    }
                }
            }
        }
    }

    private void MapHealthConditions(DynamoParticipant source, Participant participant,
        Participant? existingParticipant = null)
    {
        var healthConditions = source.HealthConditionInterests?.Select(code => new ParticipantHealthCondition
        {
            HealthConditionId = _refDataService.GetHealthConditionId(code),
            ParticipantId = participant.Id,
            CreatedAt = existingParticipant == null
                ? _clock.Now()
                : participant.HealthConditions
                    .First(x => x.HealthConditionId == _refDataService.GetHealthConditionId(code)).CreatedAt,
            UpdatedAt = _clock.Now(),
            IsDeleted = false
        }).ToList();

        if (healthConditions != null)
        {
            foreach (var healthCondition in healthConditions.Where(healthCondition =>
                         participant.HealthConditions.All(hc =>
                             hc.HealthConditionId != healthCondition.HealthConditionId)))
            {
                participant.HealthConditions.Add(healthCondition);
            }
        }
    }


    public Participant Map(Dictionary<string, AttributeValue> record, Participant? existingParticipant = null)
    {
        var doc = Document.FromAttributeMap(record);

        var source = _context.FromDocument<DynamoParticipant>(doc);

        var participant = existingParticipant ?? new Participant();

        participant.Email = source.Email;
        participant.FirstName = source.Firstname;
        participant.LastName = source.Lastname;
        participant.RegistrationConsent = source.ConsentRegistration;
        participant.RegistrationConsentAtUtc = source.ConsentRegistrationAtUtc;
        participant.MobileNumber = source.MobileNumber;
        participant.LandlineNumber = source.LandlineNumber;
        participant.DateOfBirth = source.DateOfBirth;
        participant.EthnicGroup = source.EthnicGroup;
        participant.EthnicBackground = source.EthnicBackground;
        participant.CreatedAt = source.CreatedAtUtc;
        participant.UpdatedAt = source.UpdatedAtUtc;
        participant.GenderIsSameAsSexRegisteredAtBirth = source.GenderIsSameAsSexRegisteredAtBirth;
        participant.NHSNumber = source.NhsNumber;
        participant.RemovalOfConsentRegistrationAtUtc = source.RemovalOfConsentRegistrationAtUtc;
        participant.HasLongTermCondition = source.Disability;
        participant.GenderId = _refDataService.GetGenderId(source.SexRegisteredAtBirth);
        participant.CommunicationLanguageId = _refDataService.GetCommunicationLanguageId(source.SelectedLocale);
        participant.DailyLifeImpactId = _refDataService.GetDailyLifeImpactId(source.DisabilityDescription);
        participant.Address = ParticipantAddressMapper.Map(source.Address, participant.Id);
        
        MapHealthConditions(source, participant, existingParticipant);
        MapIdentifiers(source, participant);

        return participant;
    }
}
