using System.ComponentModel.DataAnnotations;

namespace DYNAMO.STREAM.HANDLER.Entities;

public class Participant
{
    // schema for aurora db
    [Key]
    public int Id { get; set; }
    public string ParticipantId { get; set; }
    public string NhsId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public bool RegistrationConsent { get; set; }
    public DateTime RegistrationConsentAtUtc { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Email { get; set; }
    public string EthnicBackground { get; set; }
    public string EthnicGroup { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    public bool HasLongTermCondition { get; set; }
    
    public bool GenderIsSameAsSexRegisteredAtBirth { get; set; }
    public string MobileNumber { get; set; }
    public string LandlineNumber { get; set; }
    public string NHSNumber { get; set; }
    
    public bool IsDeleted { get; set; }
    public string Status { get; set; }
    public bool? Disability { get; set; }
    public string DisabilityDescription { get; set; }
    public int SexRegisteredAtBirthId { get; set; }
    public int DailyLifeImpactId { get; set; }
    public int CommunicationLanguageId { get; set; }
    public DailyLifeImpact DailyLifeImpact { get; set; }
    public CommunicationLanguage CommunicationLanguage { get; set; }
    public SexRegisteredAtBirth SexRegisteredAtBirth { get; set; }
    public ICollection<ParticipantHealthCondition> HealthConditions { get; set; } =
        new List<ParticipantHealthCondition>();
    public ICollection<ParticipantAddress> Addresses { get; set; } = new List<ParticipantAddress>();

}
