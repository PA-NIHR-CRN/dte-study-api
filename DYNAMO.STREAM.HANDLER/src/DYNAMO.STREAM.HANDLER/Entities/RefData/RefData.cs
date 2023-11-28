using System.ComponentModel.DataAnnotations;
using DYNAMO.STREAM.HANDLER.Contracts;

namespace DYNAMO.STREAM.HANDLER.Entities.RefData;

public abstract class RefData : IReferenceData, ISoftDelete
{
    protected RefData()
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
