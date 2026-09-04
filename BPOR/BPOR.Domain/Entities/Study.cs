using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class Study : ISoftDelete, IAudit
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? EmailAddress { get; set; }
    public string? StudyName { get; set; }
    public string? Description { get; set; }
    public long? CpmsId { get; set; }
    public bool IsRecruitingIdentifiableParticipants { get; set; }
    public string? ChiefInvestigator { get; set; }
    public string? ChiefInvestigatorEmail { get; set; }
    public string? Sponsors { get; set; }
    public bool? AlreadyOpenToRecruitment { get; set; }
    public int? ParticipantsRecruited { get; set; }
    public DateTime? RecruitmentStartDate { get; set; }
    public DateTime? RecruitmentEndDate { get; set; }
    public string? RecruitmentTarget { get; set; }
    public string? TargetPopulation { get; set; }
    public string? FundingCode { get; set; }
    public string? InformationUrl { get; set; }
    public bool? HasMultipleResearchLocations { get; set; }
    public bool? SinglePersonResponsibleForRecruiting { get; set; }
    public string? PreScreenerUrl { get; set; }
    public bool? HasEthicsApproval { get; set; }
    public string? MainContactRole { get; set; }
    public string? InclusionCriteria { get; set; }
    
    public SubmittedType? SubmittedId { get; set; }
    public int? SubmissionOutcomeId { get; set; }
    public StudyStatusType? StudyStatusId { get; set; }
    public NihrFundingStatusType? HasNihrFunding { get; set; }
    
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CreatedById { get; set; }
    public int UpdatedById { get; set; }

    public Submitted? Submitted { get; set; }
    public SubmissionOutcome? SubmissionOutcome { get; set; }
    public StudyStatus? StudyStatus { get; set; }
    public NihrFundingStatus? NihrFundingStatus { get; set; }
    
    public ICollection<ManualEnrollment> ManualEnrollments { get; set; } = new List<ManualEnrollment>();
    public ICollection<StudyParticipantEnrollment> StudyParticipantEnrollments { get; set; } = new List<StudyParticipantEnrollment>();
    public ICollection<FilterCriteria> FilterCriterias { get; set; } = new List<FilterCriteria>();
    public ICollection<StudyResearcher> StudyResearchers { get; set; } = new List<StudyResearcher>();
    public ICollection<StudyResearcherEmail> StudyResearcherEmails { get; } = new List<StudyResearcherEmail>();
    public ICollection<StudyStatusHistory> StudyStatusHistories { get; } = new List<StudyStatusHistory>();
}