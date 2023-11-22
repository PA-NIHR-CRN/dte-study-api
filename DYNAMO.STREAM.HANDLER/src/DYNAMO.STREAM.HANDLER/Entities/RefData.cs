using System.ComponentModel.DataAnnotations;

namespace DYNAMO.STREAM.HANDLER.Entities;

public class RefData
{
    [Key]
    public int Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
}
