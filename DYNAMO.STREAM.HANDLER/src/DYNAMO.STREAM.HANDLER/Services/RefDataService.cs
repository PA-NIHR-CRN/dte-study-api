using DYNAMO.STREAM.HANDLER.Contracts;
using DYNAMO.STREAM.HANDLER.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DYNAMO.STREAM.HANDLER.Services;

public class RefDataService : IRefDataService
{
    private readonly ILogger<RefDataService> _logger;
    private readonly Lazy<List<Gender>> _genderRefData;
    private readonly Lazy<List<HealthCondition>> _healthConditionRefData;
    private readonly Lazy<List<IdentifierType>> _identifierTypeRefData;
    private readonly Lazy<List<CommunicationLanguage>> _communicationLanguageRefData;
    private readonly Lazy<List<DailyLifeImpact>> _dailyLifeImpactRefData;

    public RefDataService(ParticipantDbContext dbContext, ILogger<RefDataService> logger)
    {
        _logger = logger;
        // Probably should lock the lazy loader so this shared resource is thread-safe. 

        // Lazy loader is thread safe by default
        _genderRefData = new Lazy<List<Gender>>(() => dbContext.Genders.AsNoTracking().ToList());
        _healthConditionRefData = new Lazy<List<HealthCondition>>(() => dbContext.HealthConditions.AsNoTracking().ToList());
        _identifierTypeRefData = new Lazy<List<IdentifierType>>(() => dbContext.IdentifierTypes.AsNoTracking().ToList());
        _communicationLanguageRefData = new Lazy<List<CommunicationLanguage>>(() => dbContext.CommunicationLanguages.AsNoTracking().ToList());
        _dailyLifeImpactRefData = new Lazy<List<DailyLifeImpact>>(() => dbContext.DailyLifeImpacts.AsNoTracking().ToList());

    }

    private int GetIdFromReferenceData<T>(Lazy<List<T>> refDataLazy, string code) where T : IReferenceData
    {
        code = code.Trim();
        // Ensure the database is case insensitive (is case-sensitivity required?)

        var refData = refDataLazy.Value;

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
