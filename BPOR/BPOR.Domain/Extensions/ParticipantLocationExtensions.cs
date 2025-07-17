using BPOR.Domain.Entities;
using BPOR.Domain.Entities.Configuration;
using NetTopologySuite.Geometries;
using NIHR.Geometry;
using NIHR.Infrastructure.Models;

namespace BPOR.Domain.Extensions;

public static class ParticipantLocationExtensions
{

    public static void SetLocationFromLatLong(this ParticipantLocation target, CoordinatesModel location)
    {
        target.SetLocationFromLatLong(location.Latitude, location.Longitude);
    }

    public static void SetLocationFromLatLong(this ParticipantLocation target, double latitude, double longitude)
    {
        target.SetLocationFromLatLong(new Point(longitude, latitude) { SRID = ParticipantLocationConfiguration.LocationSrid });
    }

    private static void SetLocationFromLatLong(this ParticipantLocation target, Point location)
    {
        target.Location = location;
        UpdateOsgbToMatchLocation(target);
    }

    public static void UpdateOsgbToMatchLocation(this ParticipantLocation target)
    {
        if (target.Location == Point.Empty)
        {
            target.Easting = 0;
            target.Northing = 0;
        }
        else
        {
            var osgbRef = Osgb6.FromLatLong(target.Location.Y, target.Location.X);
            target.Easting = osgbRef.Easting;
            target.Northing = osgbRef.Northing;
        }
    }
}