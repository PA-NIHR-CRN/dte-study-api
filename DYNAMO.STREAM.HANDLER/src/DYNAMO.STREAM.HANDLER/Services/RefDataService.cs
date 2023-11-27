using DYNAMO.STREAM.HANDLER.Contracts;
using DYNAMO.STREAM.HANDLER.Entities;
using Microsoft.Extensions.Logging;

namespace DYNAMO.STREAM.HANDLER.Services;

public class RefDataService : IRefDataService
{
    private readonly ParticipantDbContext _dbContext;
    private readonly ILogger<RefDataService> _logger;
    private readonly List<Gender> _genderRefData;
    private readonly List<HealthCondition> _healthConditionRefData;
    private readonly List<IdentifierType> _identifierTypeRefData;
    private readonly List<CommunicationLanguage> _communicationLanguageRefData;
    private readonly List<DailyLifeImpact> _dailyLifeImpactRefData;
    
    public RefDataService(ParticipantDbContext dbContext, ILogger<RefDataService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
        
        _genderRefData = _dbContext.Genders.ToList();
        _healthConditionRefData = _dbContext.HealthConditions.ToList();
        _identifierTypeRefData = _dbContext.IdentifierTypes.ToList();
        _communicationLanguageRefData = _dbContext.CommunicationLanguages.ToList();
        _dailyLifeImpactRefData = _dbContext.DailyLifeImpacts.ToList();
    }
    
    private int? GetIdFromReferenceData<T>(List<T> refData, string code, string logMessage) where T : IReferenceData
    {
        var entry = refData.FirstOrDefault(item => item.Code == code);

        if (entry == null)
        {
            _logger.LogError(logMessage, code);
            return null;
        }

        return entry.Id;
    }
    
    public int? GetGenderId(string gender)
    {
        return GetIdFromReferenceData(_genderRefData, gender, "Gender code '{0}' not found in reference data");
    }

    public int? GetDailyLifeImpactId(string impact)
    {
        return GetIdFromReferenceData(_dailyLifeImpactRefData, impact, "Daily life impact code '{0}' not found in reference data");
    }

    public int? GetHealthConditionId(string healthCondition)
    {
        return GetIdFromReferenceData(_healthConditionRefData, healthCondition, "Health condition code '{0}' not found in reference data");
    }

    public int? GetIdentifierTypeId(string identifierType)
    {
        return GetIdFromReferenceData(_identifierTypeRefData, identifierType, "Identifier type code '{0}' not found in reference data");
    }

    public int? GetCommunicationLanguageId(string language)
    {
        return GetIdFromReferenceData(_communicationLanguageRefData, language, "Communication language code '{0}' not found in reference data");
    }
}
