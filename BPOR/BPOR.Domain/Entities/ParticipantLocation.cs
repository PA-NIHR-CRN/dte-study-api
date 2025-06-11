using BPOR.Domain.Entities.Configuration;
using NetTopologySuite.Geometries;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class ParticipantLocation : ISoftDelete, ITimestamped, IPersonalInformation
{
    public int Id { get; set; }
    public Point Location { get; set; } = Point.Empty;
    public bool IsApproximate { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int ParticipantId { get; set; } 
    public Participant Participant { get; set; }
    public void Anonymise()
    {
        Location = Point.Empty;
        Location.SRID = ParticipantLocationConfiguration.LocationSrid;
    }
}
