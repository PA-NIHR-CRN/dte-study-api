using BPOR.Domain.Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

[Index(nameof(Easting), nameof(Northing))]
public class ParticipantLocation : ISoftDelete, ITimestamped, IPersonalInformation
{
    public int Id { get; set; }
    public Point Location { get; set; } = Point.Empty;
    public bool IsApproximate { get; set; } = false;
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int ParticipantId { get; set; } 
    public Participant Participant { get; set; }
    /// <summary>
    /// OSGB 6 digit Easting
    /// </summary>
    public int Easting { get; set; }
    /// <summary>
    /// OSGB 6 digit Northing
    /// </summary>
    public int Northing { get; set; }
    
    public void Anonymise()
    {
        Location = new Point(0, 0) { SRID = ParticipantLocationConfiguration.LocationSrid };
        Easting = 0;
        Northing = 0;
        IsApproximate = true;
    }
}