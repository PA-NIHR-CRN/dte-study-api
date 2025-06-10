using NetTopologySuite.Geometries;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class ParticipantLocation : ISoftDelete, ITimestamped, IPersonalInformation
{
    public int Id { get; set; }
    public Point Location { get; set; }
    /// <summary>
    /// OSGB 6 digit Easting
    /// </summary>
    public int Easting { get; set; }
    /// <summary>
    /// OSGB 6 digit Northing
    /// </summary>
    public int Northing { get; set; }
    public bool IsApproximate { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int ParticipantId { get; set; } 
    public Participant Participant { get; set; }
    public void Anonymise()
    {
        Location = null!;
    }
}
