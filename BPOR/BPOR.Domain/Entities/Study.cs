using System.ComponentModel.DataAnnotations;
using NIHR.Infrastructure.Entities;

namespace BPOR.Domain.Entities;

public class Study : ISoftDelete, ITimestamped
{
    [Key] public int Id { get; set; }

    [MaxLength(255)] public string FullName { get; set; }
    [MaxLength(255)] public string EmailAddress { get; set; }

    [MaxLength(255)] public string StudyName { get; set; }

    public long CpmsId { get; set; }
    public bool IsAnonymous { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
