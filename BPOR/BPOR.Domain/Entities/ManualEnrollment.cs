using System.ComponentModel.DataAnnotations;
using NIHR.Infrastructure.Entities;

namespace BPOR.Domain.Entities;

public class ManualEnrollment : ITimestamped, ISoftDelete
{
    public int Id { get; set; }
    public int StudyId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    [Required]
    public Study Study { get; set; }
}
