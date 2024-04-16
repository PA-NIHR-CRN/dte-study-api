using System.ComponentModel.DataAnnotations;
using BPOR.Domain.Entities.RefData;

namespace BPOR.Domain.Entities;

public class FilterCriteria
{
    public int Id { get; set; }
    public int? StudyId { get; set; }
    public Study? Study { get; set; }
    public bool? Contacted { get; set; }
    public bool? RegisteredInterest { get; set; }
    public bool? CompletedRegistration { get; set; }
    public bool? Recruited { get; set; }
    public ICollection<ParticipantHealthCondition>? HealthConditions { get; set; }
    public ICollection<string>? PostcodeDistricts { get; set; }
    public string? FullPostcode { get; set; }
    public decimal? SearchRadiusMiles { get; set; }
    public DateTime? RegistrationFromDate { get; set; }
    public DateTime? RegistrationToDate { get; set; }
    public DateTime? DateOfBirthFrom { get; set; }
    public DateTime? DateOfBirthTo { get; set; }
    public int? GenderId { get; set; }
    public Gender Gender { get; set; }
    public bool? GenderIsSameAsSexRegisteredAtBirth { get; set; }
    [MaxLength(255)] public string? EthnicGroup { get; set; }
}
