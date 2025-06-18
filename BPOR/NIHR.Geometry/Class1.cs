using GeoUK;
using GeoUK.Coordinates;
using GeoUK.Ellipsoids;
using GeoUK.Projections;

namespace NIHR.Geometry
{

    /// <summary>
    /// Holds a 6-digit OSGB grid reference - Easting and Northing units are metres.
    /// </summary>
    public struct Osgb6
    {
        public static Osgb6 FromLatLong(double latitude, double longitude)
        {
            LatitudeLongitude latLong = new LatitudeLongitude(latitude, longitude);

            Cartesian cartesian = GeoUK.Convert.ToCartesian(new Wgs84(), latLong);
            Cartesian bngCartesian = Transform.Etrs89ToOsgb36(cartesian);
            EastingNorthing bngEN = GeoUK.Convert.ToEastingNorthing(new Airy1830(), new BritishNationalGrid(), bngCartesian);

            return new Osgb6((int)bngEN.Easting, (int)bngEN.Northing);
        }

        public readonly int Easting;
        public readonly int Northing;

        public Osgb6(int easting, int northing)
        {
            Easting = easting;
            Northing = northing;
        }

        public double DistanceTo(Osgb6 other)
        {
            long de = Easting - other.Easting;
            long dn = Northing - other.Northing;
            return Math.Sqrt(de * de + dn * dn);
        }

        public override string ToString()
        {
            return $"{Easting:000000}E {Northing:000000}N";
        }
    }
}
