using System.ComponentModel.DataAnnotations.Schema;

namespace BPOR.Domain.Entities;

[Table("StreamEvent")]
public class StreamEvent
{
    public string Id { get; set; } = default!;
    public bool IsProcessing { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
}