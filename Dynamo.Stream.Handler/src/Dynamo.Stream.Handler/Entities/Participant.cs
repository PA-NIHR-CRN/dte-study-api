using Dynamo.Stream.Handler.Entities.RefData;
using System.ComponentModel.DataAnnotations;

namespace Dynamo.Stream.Handler.Entities;

public class Participant : ISoftDelete, ITimestamped, IPersonalInformation
{
    public Participant()
    {
        DailyLifeImpact = null!;
        CommunicationLanguage = null!;
        Gender = null!;
        Address = null!;
    }

    [Key]
    public int Id { get; set; }

    [MaxLength(255)]
    public string? FirstName { get; set; }

    [MaxLength(255)]
    public string? LastName { get; set; }

    public bool RegistrationConsent { get; set; }
    public DateTime? RegistrationConsentAtUtc { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [MaxLength(255)]
    public string? Email { get; set; }
    [MaxLength(255)]
    public string? EthnicBackground { get; set; }
    [MaxLength(255)]
    public string? EthnicGroup { get; set; }

    public DateTime? DateOfBirth { get; set; }
    public DateTime? RemovalOfConsentRegistrationAtUtc { get; set; }
    public bool? HasLongTermCondition { get; set; }

    public bool? GenderIsSameAsSexRegisteredAtBirth { get; set; }
    [MaxLength(255)]
    public string? MobileNumber { get; set; }
    [MaxLength(255)]
    public string? LandlineNumber { get; set; }
    [MaxLength(255)]
    public string? NHSNumber { get; set; }

    public bool IsDeleted { get; set; }
    public int? DailyLifeImpactId { get; set; }
    public int? CommunicationLanguageId { get; set; }
    public int? GenderId { get; set; }

    public DailyLifeImpact DailyLifeImpact { get; set; }
    public CommunicationLanguage CommunicationLanguage { get; set; }
    public Gender Gender { get; set; }
    public ICollection<ParticipantHealthCondition> HealthConditions { get; set; } =
        new List<ParticipantHealthCondition>();
    public ICollection<ParticipantIdentifier> ParticipantIdentifiers { get; set; } =
        new List<ParticipantIdentifier>();
    public ParticipantAddress? Address { get; set; }

    public ICollection<SourceReference> SourceReferences { get; set; } = new List<SourceReference>();

    public void Anonymise()
    {
        Email = null;
        FirstName = null;
        LastName = null;
        MobileNumber = null;
        LandlineNumber = null;
        RegistrationConsent = false;
        RemovalOfConsentRegistrationAtUtc = DateTime.UtcNow;
        HasLongTermCondition = null; // TODO: confirm we are clearing this but not DailyLifeImpact?
        Address?.Anonymise();
        HealthConditions.Clear();
    }
}
