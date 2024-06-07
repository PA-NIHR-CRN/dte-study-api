using CsvHelper.Configuration;

namespace GeolocationDataImport
{
    public sealed class GeoDataMap : ClassMap<GeoData>
    {
        public GeoDataMap()
        {
            Map(m => m.Postcode).Index(0);
            Map(m => m.CountryCode).Index(16);
            Map(m => m.Latitude).Index(42);
            Map(m => m.Longitude).Index(43);
            Map(m => m.DOTERM).Name("doterm");
        }
    }
}
