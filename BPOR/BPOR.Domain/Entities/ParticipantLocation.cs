using BPOR.Domain.Entities.Configuration;
using Contentful.Core.Models;
using NetTopologySuite.Geometries;
using NIHR.Geometry;
using NIHR.Infrastructure.EntityFrameworkCore;
using NIHR.Infrastructure.Models;

namespace BPOR.Domain.Entities;

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
        SetLocation(Point.Empty);
        IsApproximate = true;
    }

    public static ParticipantLocation FromCoordinates(CoordinatesModel coordinates)
    {
        var result = new ParticipantLocation();
        result.SetLocation(coordinates);
        return result;
    }

    public static ParticipantLocation FromPoint(Point point)
    {
        var result = new ParticipantLocation();
        result.SetLocation(point);
        return result;
    }

    public static ParticipantLocation FromLatLong(double latitude, double longitude)
    {
        var result = new ParticipantLocation();
        result.SetLocation(latitude, longitude);
        return result;

    }
    public void SetLocation(CoordinatesModel location) => SetLocation(location.Latitude, location.Longitude);

    public void SetLocation(double latitude, double longitude) => SetLocation(new Point(longitude, latitude) { SRID = ParticipantLocationConfiguration.LocationSrid });

    public void SetLocation(Point location)
    {
        Location = location;
        SetOsgbFromLocation();
    }

    public void SetOsgbFromLocation()
    {
        if (Location == Point.Empty)
        {
            Easting = 0;
            Northing = 0;
        }
        else
        {
            var osgbRef = Osgb6.FromLatLong(Location.Y, Location.X);
            Easting = osgbRef.Easting;
            Northing = osgbRef.Northing;
        }
    }
}
