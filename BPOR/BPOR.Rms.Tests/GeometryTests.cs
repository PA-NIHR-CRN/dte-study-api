using NIHR.Geometry;

namespace BPOR.Rms.Tests;

public class GeometryTests
{
    [Fact]
    public void TestOsgbFromLatLong()
    {
        var belfastOffice = Osgb.FromLongitudeLatitude(-5.9335271, 54.598746);
        Assert.Equal(new Osgb6(146029, 529733), belfastOffice);

        var londonOffice = Osgb.FromLongitudeLatitude(-0.14262605, 51.498148);
        Assert.Equal(new Osgb6(529025, 179325), londonOffice);
    }
}
