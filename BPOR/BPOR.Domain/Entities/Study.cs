using System.ComponentModel.DataAnnotations;
using NIHR.Infrastructure.Entities;

namespace BPOR.Domain.Entities;

public class Study : ISoftDelete, ITimestamped
{
    [Key]
    [Display(Name = "Study ID")]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Main contact")]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email address")]
    public string EmailAddress { get; set; }

    [Required]
    [Display(Name = "Study name")]
    public string StudyName { get; set; }

    [Display(Name = "CPMS ID")]
    public string CpmsId { get; set; }
    public bool IsAnonymous { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
