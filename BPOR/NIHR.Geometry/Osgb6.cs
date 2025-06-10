namespace NIHR.Geometry
{
    public struct Osgb6
    {
        public readonly int Easting;
        public readonly int Northing;

        public Osgb6(int east, int north)
        {
            Easting = east;
            Northing = north;
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
