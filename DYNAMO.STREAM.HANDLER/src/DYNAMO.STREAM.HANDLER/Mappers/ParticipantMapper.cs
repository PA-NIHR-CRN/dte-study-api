using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using DYNAMO.STREAM.HANDLER.Entities;
using DYNAMO.STREAM.HANDLER.Services;
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
        // Get dictionary of identifiers from source
        var identifiers = ExtractIdentifiers(new Dictionary<string, AttributeValue>
        {
            { "ParticipantId", new AttributeValue { S = source.ParticipantId } },
            { "NhsId", new AttributeValue { S = source.NhsNumber } }
        });

        foreach (var identifier in identifiers)
        {
            if (string.IsNullOrWhiteSpace(identifier.Value)) continue;

            if (!participant.ParticipantIdentifiers.Any(pi => pi.Value == identifier.Value && pi.IdentifierTypeId == identifier.Type))
            {
                var newIdentifier = new ParticipantIdentifier
                {
                    Value = identifier.Value,
                    IdentifierTypeId = identifier.Type,
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

        // Add new health conditions
        foreach (var sourceHealthCondition in sourceHealthConditions.Where(sourceHealthCondition =>
                     participant.HealthConditions.All(hc =>
                         hc.HealthConditionId != sourceHealthCondition.HealthConditionId)))
        {
            participant.HealthConditions.Add(sourceHealthCondition);
        }
        
        // Identify health conditions to be removed
        var healthConditionsToRemove = participant.HealthConditions
            .Where(destinationHealthCondition =>
                !sourceHealthConditions.Exists(hc =>
                    hc.HealthConditionId == destinationHealthCondition.HealthConditionId))
            .ToList();

        // Remove identified health conditions
        foreach (var healthCondition in healthConditionsToRemove)
        {
            participant.HealthConditions.Remove(healthCondition);
        }
    }

    public Participant Map(Dictionary<string, AttributeValue> record, Participant destination)
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

        return destination;
    }

    public List<Identifier> ExtractIdentifiers(Dictionary<string, AttributeValue> newImage)
    {
        var keyNames = new[] { "ParticipantId", "NhsId" };
        var identifiers = new List<Identifier>();

        foreach (var keyName in keyNames)
        {
            if (newImage.TryGetValue(keyName, out var attrValue) && !string.IsNullOrWhiteSpace(attrValue.S))
            {
                int typeId = _refDataService.GetIdentifierTypeId(keyName);
                identifiers.Add(new Identifier(typeId, attrValue.S));
            }
        }

        return identifiers;
    }

}
