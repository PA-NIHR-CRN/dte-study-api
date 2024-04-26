using System.ComponentModel.DataAnnotations;
using BPOR.Domain.Entities.RefData;

namespace BPOR.Domain.Entities;

public class FilterCriteria
{
    public int Id { get; set; }
    public int? StudyId { get; set; }
    public Study? Study { get; set; }
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
    public ICollection<EmailCampaign>? EmailCampaigns { get; set; } = new List<EmailCampaign>();
    public ICollection<FilterAreaOfInterest>? FilterAreaOfInterest { get; set; } = new List<FilterAreaOfInterest>();
    public ICollection<FilterPostcode>? FilterPostcode { get; set; } = new List<FilterPostcode>();
    public ICollection<FilterGender>? FilterGender { get; set; } = new List<FilterGender>();
    public ICollection<FilterSexSameAsRegisteredAtBirth>? FilterSexSameAsRegisteredAtBirth { get; set; } = new List<FilterSexSameAsRegisteredAtBirth>();
    public ICollection<FilterEthnicGroup>? FilterEthnicGroup { get; set; } = new List<FilterEthnicGroup>();
}

