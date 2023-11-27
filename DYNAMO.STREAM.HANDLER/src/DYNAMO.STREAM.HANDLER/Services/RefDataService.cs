using DYNAMO.STREAM.HANDLER.Contracts;
using DYNAMO.STREAM.HANDLER.Entities;
using Microsoft.Extensions.Logging;

namespace DYNAMO.STREAM.HANDLER.Services;

public class RefDataService : IRefDataService
{
    private readonly ILogger<RefDataService> _logger;
    private readonly List<Gender> _genderRefData;
    private readonly List<HealthCondition> _healthConditionRefData;
    private readonly List<IdentifierType> _identifierTypeRefData;
    private readonly List<CommunicationLanguage> _communicationLanguageRefData;
    private readonly List<DailyLifeImpact> _dailyLifeImpactRefData;

    public RefDataService(ParticipantDbContext dbContext, ILogger<RefDataService> logger)
    {
        _logger = logger;

        // TODO: make these lazy. Not all execution paths will need all the reference data
        // load them only when needed and then keep them around.
        // Probably should lock the lazy loader so this shared resource is thread-safe.

        // Load these as no-tracking. We won't be updating them.
        _genderRefData = dbContext.Genders.ToList();
        _healthConditionRefData = dbContext.HealthConditions.ToList();
        _identifierTypeRefData = dbContext.IdentifierTypes.ToList();
        _communicationLanguageRefData = dbContext.CommunicationLanguages.ToList();
        _dailyLifeImpactRefData = dbContext.DailyLifeImpacts.ToList();
    }

    private int GetIdFromReferenceData<T>(List<T> refData, string code) where T : IReferenceData
    {
        // TODO: Any trimming of code required here?
        // Ensure the database is case insensitive (is case-sensitivity required?)

        var matches = refData.Where(item => item.Code == code)
                            .OrderBy(x => x.IsDeleted ? 1 : 0) // Use active matches (0) first
                            .ThenBy(x => x.Id);

        if (!matches.Any())
        {
            throw new KeyNotFoundException($"Reference data '{code}' not found.");
        }

        if (matches.Count(x => !x.IsDeleted) > 1)
        {
            // TODO: Clarify requirements. Is this a hard stop or should we allow multiple active matches?
            throw new KeyNotFoundException($"More than one active match for reference data code '{code}'.");
        }

        if (matches.Count() > 1)
        {
            _logger.LogWarning("Reference data '{code}' has multiple matches.", code);
        }

        var entry = matches.First();

        if (entry.IsDeleted)
        {
            _logger.LogWarning("Mapping reference data to deleted record '{code}' => '{id}'.", code, entry.Id);
        }

        return entry.Id;
    }

    public int? GetGenderId(string gender)
    {
        using (_logger.BeginScope(nameof(GetGenderId)))
        {
            if (string.IsNullOrWhiteSpace(gender))
            {
                return null;
            }

            return GetIdFromReferenceData(_genderRefData, gender);
        }
    }

    public int? GetDailyLifeImpactId(string impact)
    {
        using (_logger.BeginScope(nameof(GetDailyLifeImpactId)))
        {
            if (string.IsNullOrWhiteSpace(impact))
            {
                return null;
            }
            return GetIdFromReferenceData(_dailyLifeImpactRefData, impact);
        }
    }

    public int GetHealthConditionId(string healthCondition)
    {
        using (_logger.BeginScope(nameof(GetHealthConditionId)))
        {
            return GetIdFromReferenceData(_healthConditionRefData, healthCondition);
        }
    }

    public int GetIdentifierTypeId(string identifierType)
    {
        using (_logger.BeginScope(nameof(GetIdentifierTypeId)))
        {
            return GetIdFromReferenceData(_identifierTypeRefData, identifierType);
        }
    }

    public int? GetCommunicationLanguageId(string language)
    {
        using (_logger.BeginScope(nameof(GetCommunicationLanguageId)))
        {
            if (string.IsNullOrWhiteSpace(language))
            {
                return null;
            }

            return GetIdFromReferenceData(_communicationLanguageRefData, language);
        }
    }
}
