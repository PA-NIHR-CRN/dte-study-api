using System.ComponentModel.DataAnnotations;
using DYNAMO.STREAM.HANDLER.Contracts;

namespace DYNAMO.STREAM.HANDLER.Entities;

public class RefData : IReferenceData
{
    [Key]
    public int Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
}
