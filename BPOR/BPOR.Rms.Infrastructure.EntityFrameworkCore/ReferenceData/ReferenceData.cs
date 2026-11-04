using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Infrastructure.EntityFrameworkCore;

public abstract class ReferenceData : IReferenceData, ISoftDelete
{
    protected ReferenceData()
    {
        Code = null!;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Code { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    public bool IsDeleted { get; set; }
}