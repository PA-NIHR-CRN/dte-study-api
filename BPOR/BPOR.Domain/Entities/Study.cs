using System.ComponentModel.DataAnnotations;
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
