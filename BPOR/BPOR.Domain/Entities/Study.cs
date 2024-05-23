using System.ComponentModel.DataAnnotations;
using BPOR.Domain.Entities.RefData;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class Study : ISoftDelete, IAudit
{
    [Key] public int Id { get; set; }

    [MaxLength(255)] public string FullName { get; set; }
    [MaxLength(255)] public string EmailAddress { get; set; }

    [MaxLength(255)] public string StudyName { get; set; }

    public long? CpmsId { get; set; }
    public bool IsRecruitingIdentifiableParticipants { get; set; }
    public string? ChiefInvestigator { get; set; }
    public string? Sponsors { get; set; }
    public bool? AlreadyOpenToRecruitment { get; set; }
    public int? ParticipantsRecruited { get; set; }
    public DateTime? RecruitmentStartDate { get; set; }
    public DateTime? RecruitmentEndDate { get; set; }
    public int? RecruitmentTarget { get; set; }
    public string? TargetPopulation { get; set; }
    public bool? HasNihrFunding { get; set; }
    public string? FundingCode { get; set; }
    public Submitted? Submitted { get; set; }
    public SubmissionOutcome? SubmissionOutcome { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CreatedById { get; set; }
    public int UpdatedById { get; set; }
    public ICollection<ManualEnrollment> ManualEnrollments { get; set; } = new List<ManualEnrollment>();
    public ICollection<StudyParticipantEnrollment> StudyParticipantEnrollments { get; set; } = new List<StudyParticipantEnrollment>();
    public ICollection<FilterCriteria> FilterCriterias { get; set; }  = new List<FilterCriteria>();
    public ICollection<StudyResearcher> StudyResearchers { get; set; } = new List<StudyResearcher>();
    
}
