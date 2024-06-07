using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationDataImport
{
    public class GeoData
    {
        public string Postcode { get; set; }
        public string DOTERM { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string CountryCode { get; set; }
        [NotMapped]
        public string CountryName { get; set; }
    }
}
