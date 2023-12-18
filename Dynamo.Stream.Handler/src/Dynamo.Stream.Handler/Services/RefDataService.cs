using System.Reflection;
using Dynamo.Stream.Handler.Entities;
using Dynamo.Stream.Handler.Entities.RefData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Dynamo.Stream.Handler.Services;

public class RefDataService : IRefDataService
{
    private readonly ILogger<RefDataService> _logger;
    private readonly Lazy<List<Gender>> _genderRefData;
    private readonly Lazy<List<HealthCondition>> _healthConditionRefData;
    private readonly Lazy<List<IdentifierType>> _identifierTypeRefData;
    private readonly Lazy<List<CommunicationLanguage>> _communicationLanguageRefData;
    private readonly Lazy<List<DailyLifeImpact>> _dailyLifeImpactRefData;
    private readonly object _lockObject = new object();
    private readonly ParticipantDbContext _dbContext;

    public RefDataService(ParticipantDbContext dbContext, ILogger<RefDataService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;

        _genderRefData = new Lazy<List<Gender>>(() =>
        {
            lock (_lockObject)
            {
                return dbContext.Genders.AsNoTracking().ToList();
            }
        });

        _healthConditionRefData = new Lazy<List<HealthCondition>>(() =>
        {
            lock (_lockObject)
            {
                return dbContext.HealthConditions.AsNoTracking().ToList();
            }
        });

        _identifierTypeRefData = new Lazy<List<IdentifierType>>(() =>
        {
            lock (_lockObject)
            {
                return dbContext.IdentifierTypes.AsNoTracking().ToList();
            }
        });

        _communicationLanguageRefData = new Lazy<List<CommunicationLanguage>>(() =>
        {
            lock (_lockObject)
            {
                return dbContext.CommunicationLanguages.AsNoTracking().ToList();
            }
        });

        _dailyLifeImpactRefData = new Lazy<List<DailyLifeImpact>>(() =>
        {
            lock (_lockObject)
            {
                return dbContext.DailyLifeImpacts.AsNoTracking().ToList();
            }
        });
    }
    
    private void UpdateCache<T>(T newRefData) where T : IReferenceData
    {
        switch (newRefData)
        {
            case Gender gender:
                _genderRefData.Value.Add(gender);
                break;
            case HealthCondition healthCondition:
                _healthConditionRefData.Value.Add(healthCondition);
                break;
            case IdentifierType identifierType:
                _identifierTypeRefData.Value.Add(identifierType);
                break;
            case CommunicationLanguage communicationLanguage:
                _communicationLanguageRefData.Value.Add(communicationLanguage);
                break;
            case DailyLifeImpact dailyLifeImpact:
                _dailyLifeImpactRefData.Value.Add(dailyLifeImpact);
                break;
            default:
                throw new NotSupportedException($"Unknown reference data type '{typeof(T).Name}'.");
        }
    }

    private int GetIdFromReferenceData<T>(IEnumerable<T> refData, string code) where T : IReferenceData, new()
    {
        code = code.Trim();
        // Ensure the database is case insensitive (is case-sensitivity required?)

        var matches = refData.Where(item => item.Code.Equals(code, StringComparison.OrdinalIgnoreCase))
            .OrderBy(x => x.IsDeleted ? 1 : 0) // Use active matches (0) first
            .ThenBy(x => x.Id);

        if (!matches.Any())
        {
            // Create a new instance of the reference data and add it to the database
            var newRefData = new T
            {
                Code = code,
                Description = code,
                IsDeleted = false
            };

            _dbContext.Add(newRefData);
            _dbContext.SaveChanges();

            UpdateCache(newRefData);

            return newRefData.Id;
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

            return GetIdFromReferenceData(_genderRefData.Value, gender);
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

            return GetIdFromReferenceData(_dailyLifeImpactRefData.Value, impact);
        }
    }

    public int GetHealthConditionId(string healthCondition)
    {
        using (_logger.BeginScope(nameof(GetHealthConditionId)))
        {
            return GetIdFromReferenceData(_healthConditionRefData.Value, healthCondition);
        }
    }

    public int GetIdentifierTypeId(string identifierType)
    {
        using (_logger.BeginScope(nameof(GetIdentifierTypeId)))
        {
            return GetIdFromReferenceData(_identifierTypeRefData.Value, identifierType);
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

            return GetIdFromReferenceData(_communicationLanguageRefData.Value, language);
        }
    }
}
