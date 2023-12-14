using Dynamo.Stream.Handler.Entities;
using System.ComponentModel.DataAnnotations;

namespace Dynamo.Stream.Handler.Entities.RefData;

public abstract class ReferenceData : IReferenceData, ISoftDelete
{
    protected ReferenceData()
    {
        Code = null!;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    public string Code { get; set; }
    public string? Description { get; set; }
    public bool IsDeleted { get; set; }
}
