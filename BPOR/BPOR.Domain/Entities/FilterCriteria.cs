namespace BPOR.Domain.Entities;

public class FilterCriteria
{
    public int Id { get; set; }
    public int? StudyId { get; set; }
    public bool? IncludeContacted { get; set; }
    public bool? IncludeRegisteredInterest { get; set; }
    public bool? IncludeCompletedRegistration { get; set; }
    public bool? IncludeRecruited { get; set; }
    public string? FullPostcode { get; set; }
    public double? SearchRadiusMiles { get; set; }
    public DateTime? RegistrationFromDate { get; set; }
    public DateTime? RegistrationToDate { get; set; }
    public DateTime? DateOfBirthFrom { get; set; }
    public DateTime? DateOfBirthTo { get; set; }
    public ICollection<EmailCampaign> EmailCampaigns { get; set; } = [];
    public ICollection<FilterAreaOfInterest> FilterAreaOfInterest { get; set; } = [];
    public ICollection<FilterPostcode> FilterPostcode { get; set; } = [];
    public ICollection<FilterGender> FilterGender { get; set; } = [];
    public ICollection<FilterSexSameAsRegisteredAtBirth> FilterSexSameAsRegisteredAtBirth { get; set; } = [];
    public ICollection<FilterEthnicGroup> FilterEthnicGroup { get; set; } = [];
    public bool IncludeNoAreasOfInterest { get; set; }
    public Study Study { get; set; }
}

