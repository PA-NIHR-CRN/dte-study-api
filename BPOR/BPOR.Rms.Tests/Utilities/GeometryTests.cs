using NIHR.Geometry;

namespace BPOR.Rms.Tests.Utilities;

public class GeometryTests
{
    [Fact]
    public void TestOsgbFromLatLong()
    {
        var belfastOffice = Osgb6.FromLatLong(54.598746, -5.9335271);
        Assert.Equal(new Osgb6(146029, 529733), belfastOffice);

        var londonOffice = Osgb6.FromLatLong(51.498148, -0.14262605);
        Assert.Equal(new Osgb6(529025, 179325), londonOffice);
    }
}
