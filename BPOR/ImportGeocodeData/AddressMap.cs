using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationDataImport
{
    public sealed class AddressMap : ClassMap<Address>
    {
        public AddressMap()
        {
            Map(m => m.Postcode).Index(3);
            Map(m => m.HouseNumber).Index(7);
            Map(m => m.Street).Index(9);
            Map(m => m.TownCity).Index(11);
            Map(m => m.County).Index(13);
        }
    }
}
