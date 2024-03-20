namespace BPOR.Registration.Stream.Handler.Services;

public interface IRefDataService
{
    public int? GetDailyLifeImpactId(string impact);
    public int GetHealthConditionId(string healthCondition);
    public int GetIdentifierTypeId(string identifierType);
    public int? GetCommunicationLanguageId(string language);
    public int? GetGenderId(string gender);
}
