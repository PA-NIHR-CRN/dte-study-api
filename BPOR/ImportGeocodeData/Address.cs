using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationDataImport
{
    public class Address
    {
        public string? Postcode { get; set; }
        public string? HouseNumber { get; set; }
        public string? Street { get; set; }
        public string? TownCity { get; set; }
        public string? County { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
    }
}
