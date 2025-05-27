using System.Text.Json;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime.Internal.Util;
using BPOR.Domain.Entities;
using BPOR.Domain.Entities.Configuration;
using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;
using BPOR.Domain.Extensions;
using BPOR.Infrastructure.Clients;
using BPOR.Registration.Stream.Handler.Services;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using NIHR.Infrastructure;
using NIHR.Infrastructure.Models;
using static System.Net.Mime.MediaTypeNames;

namespace BPOR.Registration.Stream.Handler.Mappers;

public class ParticipantMapper : IParticipantMapper
{
    private readonly IDynamoDBContext _context;
    private readonly IRefDataService _refDataService;
    private readonly IPostcodeMapper _locationApiClient;
    private readonly ILogger<ParticipantMapper> _logger;

    public ParticipantMapper(IDynamoDBContext context, IRefDataService refDataService,
        IPostcodeMapper locationApiClient, ILogger<ParticipantMapper> logger)
    {
        _context = context;
        _refDataService = refDataService;
        _locationApiClient = locationApiClient;
        _logger = logger;
    }

    private void MapIdentifiers(DynamoParticipant source, Participant participant)
    {
        // Get dictionary of identifiers from source
        var identifiers = ExtractIdentifiers(new Dictionary<string, AttributeValue>
        {
            { "ParticipantId", new AttributeValue { S = source.ParticipantId } },
            { "NhsId", new AttributeValue { S = source.NhsId } }
        });

        // if pk begins with deleted add identifier
        if (source.Pk.StartsWith("DELETED#"))
        {
            identifiers.Add(new Identifier((int)IdentifierTypes.Deleted, Guid.Parse(source.Pk.Split("#")[1])));
        }

        foreach (var identifier in identifiers)
        {
            if (identifier.Value == Guid.Empty) continue;

            if (!participant.ParticipantIdentifiers.Any(pi =>
                    pi.Value == identifier.Value && pi.IdentifierTypeId == identifier.Type))
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
    private void MapParticipantContactMethod(DynamoParticipant source, Participant participant)
    {
        if (participant.ContactMethodId.Count == 0) { 
            participant.ContactMethodId.Add(new ParticipantContactMethod()
            {
                ContactMethodId = (int)ContactMethodId.Email,

            });
        }
    }

    private void MapHealthConditions(DynamoParticipant source, Participant participant)
    {
        //  Stage one registration does not have health conditions
        if (source.HealthConditionInterests == null && !participant.HealthConditions.Any())
        {
            return;
        }

        // Clear existing conditions if source is empty or null
        if (source.HealthConditionInterests == null || !source.HealthConditionInterests.Any())
        {
            participant.HealthConditions.Clear();
            return;
        }

        var sourceHealthConditionIds = source.HealthConditionInterests
            .Select(code => _refDataService.GetHealthConditionId(code))
            .ToList();

        // Add new health conditions not in participant.HealthConditions
        foreach (var healthConditionId in sourceHealthConditionIds)
        {
            if (!participant.HealthConditions.Any(hc => hc.HealthConditionId == healthConditionId))
            {
                participant.HealthConditions.Add(new ParticipantHealthCondition
                {
                    HealthConditionId = healthConditionId,
                    ParticipantId = participant.Id
                });
            }
        }

        // Create a list of health conditions to be removed
        var healthConditionsToRemove = participant.HealthConditions
            .Where(hc => !sourceHealthConditionIds.Contains(hc.HealthConditionId))
            .ToList();

        // Remove the identified health conditions
        foreach (var healthCondition in healthConditionsToRemove)
        {
            participant.HealthConditions.Remove(healthCondition);
        }
    }


    public async Task<Participant> Map(Dictionary<string, AttributeValue> record, Participant destination,
        CancellationToken cancellationToken)
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
        destination.CreatedAt = source.CreatedAtUtc;
        destination.UpdatedAt = source.UpdatedAtUtc.HasValue ? source.UpdatedAtUtc.Value : source.CreatedAtUtc;
        destination.Stage2CompleteUtc = source.Stage2CompleteUtc;
        destination.IsStage2CompleteUtcBackfilled = source.IsStage2CompleteUtcBackfilled;

        if (!destination.SourceReferences.Any(x => x.Pk == record.PK()))
        {
            destination.SourceReferences.Add(new SourceReference { Pk = record.PK() });
        }

        if(source.Address != null) { 
            ParticipantAddressMapper.Map(source.Address, destination);

            if (destination.Address != null && !String.IsNullOrWhiteSpace(destination.Address.Postcode))
            {
                var coordinates =
                    await _locationApiClient.GetCoordinatesFromPostcodeAsync(destination.Address.Postcode, cancellationToken);

                if (coordinates != null)
                {
                    destination.ParticipantLocation ??= new ParticipantLocation();
                    destination.ParticipantLocation.Location = new Point(coordinates.Longitude, coordinates.Latitude)
                    { SRID = ParticipantLocationConfiguration.LocationSrid };
                }

                IEnumerable<PostcodeAddressModel> addressModels;
                addressModels = await _locationApiClient.GetAddressesByPostcodeAsync(destination.Address.Postcode, cancellationToken);

                if (addressModels.Any())
                {
                    destination.Address.CanonicalTown = addressModels.First().Town;
                }
            }
        }

        MapHealthConditions(source, destination);
        MapIdentifiers(source, destination);
        MapParticipantContactMethod(source, destination);

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
                identifiers.Add(new Identifier(typeId, Guid.Parse(attrValue.S)));
            }
        }

        return identifiers;
    }
}
