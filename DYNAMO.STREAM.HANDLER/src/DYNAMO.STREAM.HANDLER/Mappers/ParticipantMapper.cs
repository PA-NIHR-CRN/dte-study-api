using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using DYNAMO.STREAM.HANDLER.Contracts;
using DYNAMO.STREAM.HANDLER.Entities;
using DynamoParticipant = Domain.Entities.Participants.Participant;

namespace DYNAMO.STREAM.HANDLER.Mappers;

public class ParticipantMapper : IParticipantMapper
{
    private readonly IDynamoDBContext _context;
    private readonly IRefDataService _refDataService;

    public ParticipantMapper(IDynamoDBContext context, IRefDataService refDataService)
    {
        _context = context;
        _refDataService = refDataService;
    }

    private void MapIdentifiers(DynamoParticipant source, Participant participant)
    {
        // Iterate over possible identifiers (ParticipantId, NhsId, etc.)
        var identifiers = new List<(int type, string value)>
        {
            (_refDataService.GetIdentifierTypeId("ParticipantId"), source.ParticipantId),
            (_refDataService.GetIdentifierTypeId("NhsId"), source.NhsId)
        };

        foreach (var (type, value) in identifiers)
        {
            if (!participant.ParticipantIdentifiers.Any(pi => pi.Value == value && pi.IdentifierTypeId == type))
            {
                var newIdentifier = new ParticipantIdentifier
                {
                    Value = value,
                    IdentifierTypeId = type,
                };

                participant.ParticipantIdentifiers.Add(newIdentifier);
            }
        }
    }

    private void MapHealthConditions(DynamoParticipant source, Participant participant)
    {
        var sourceHealthConditions = source.HealthConditionInterests
            .Select(code => new ParticipantHealthCondition
        {
            HealthConditionId = _refDataService.GetHealthConditionId(code),
            ParticipantId = participant.Id
        }).ToList();

        // TODO: Requirements clarification. If a health condition is added, removed and then added again,
        // should a new row be added to participant.HealthConditions or should the old soft deleted row be reactivated?

        // Add new health conditions
        foreach (var sourceHealthCondition in sourceHealthConditions)
        {
            if (!participant.HealthConditions.Any(hc => hc.Id != sourceHealthCondition.Id))
            {
                participant.HealthConditions.Add(sourceHealthCondition);
            }
        }

        // Remove health conditions
        foreach (var destinationHealthCondition in participant.HealthConditions)
        {
            if (!sourceHealthConditions.Exists(hc => hc.Id != destinationHealthCondition.Id))
            {
                // TODO: Handle soft delete transparently
                participant.HealthConditions.Remove(destinationHealthCondition);
            }
        }
    }


    public void Map(Dictionary<string, AttributeValue> record, Participant destination)
    {
        var doc = Document.FromAttributeMap(record);

        var source = _context.FromDocument<DynamoParticipant>(doc);

        destination.Email = source.Email;
        destination.FirstName = source.Firstname;
        destination.LastName = source.Lastname;
        destination.RegistrationConsent = source.ConsentRegistration;
        destination.RegistrationConsentAtUtc = source.ConsentRegistrationAtUtc;
        destination.MobileNumber = source.MobileNumber;
        destination.LandlineNumber = source.LandlineNumber;
        destination.DateOfBirth = source.DateOfBirth;
        destination.EthnicGroup = source.EthnicGroup;
        destination.EthnicBackground = source.EthnicBackground;
        destination.GenderIsSameAsSexRegisteredAtBirth = source.GenderIsSameAsSexRegisteredAtBirth;
        destination.NHSNumber = source.NhsNumber;
        destination.RemovalOfConsentRegistrationAtUtc = source.RemovalOfConsentRegistrationAtUtc;
        destination.HasLongTermCondition = source.Disability;
        destination.GenderId = _refDataService.GetGenderId(source.SexRegisteredAtBirth);
        destination.CommunicationLanguageId = _refDataService.GetCommunicationLanguageId(source.SelectedLocale);
        destination.DailyLifeImpactId = _refDataService.GetDailyLifeImpactId(source.DisabilityDescription);
        destination.Address = ParticipantAddressMapper.Map(source.Address, destination.Id);

        MapHealthConditions(source, destination);
        MapIdentifiers(source, destination);
    }
}
