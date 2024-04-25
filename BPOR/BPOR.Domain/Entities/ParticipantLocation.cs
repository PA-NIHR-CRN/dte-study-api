using NetTopologySuite.Geometries;
using NIHR.Infrastructure.Entities;

namespace BPOR.Domain.Entities;

public class ParticipantLocation : ISoftDelete, ITimestamped
{
    public int Id { get; set; }
    public Point Location { get; set; }
    public bool IsApproximate { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int ParticipantId { get; set; } 
    public Participant Participant { get; set; }  
}
