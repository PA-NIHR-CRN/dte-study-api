using GeoUK;
using GeoUK.Coordinates;
using GeoUK.Ellipsoids;
using GeoUK.Projections;

namespace NIHR.Geometry
{
    public static class Osgb
    {
        public static Osgb6 FromLongitudeLatitude(double longitude, double latitude)
        {
            LatitudeLongitude latLong = new LatitudeLongitude(latitude, longitude);

            Cartesian cartesian = GeoUK.Convert.ToCartesian(new Wgs84(), latLong);
            Cartesian bngCartesian = Transform.Etrs89ToOsgb36(cartesian);
            EastingNorthing bngEN = GeoUK.Convert.ToEastingNorthing(new Airy1830(), new BritishNationalGrid(), bngCartesian);

            return new Osgb6((int)bngEN.Easting, (int)bngEN.Northing);
        }
    }
}
