using System.ComponentModel.DataAnnotations;
using NIHR.Infrastructure.Entities;

namespace BPOR.Domain.Entities.RefData;

public class ReferenceData : IReferenceData, ISoftDelete
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
