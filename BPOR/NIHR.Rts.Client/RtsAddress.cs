using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHR.Rts.Client
{
    public class RtsAddress
    {
        public RtsAddress(int id, string addressLine1, string addressLine2, string addressLine3, string addressLine4, string addressLine5, string postcode)
        {
            Id = id;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            AddressLine3 = addressLine3;
            AddressLine4 = addressLine4;
            AddressLine5 = addressLine5;
            Postcode = postcode;
        }

        public RtsAddress()
        {
        }

        public int Id { get; }
        public string AddressLine1 { get; }
        public string AddressLine2 { get; }
        public string AddressLine3 { get; }
        public string AddressLine4 { get; }
        public string AddressLine5 { get; }
        public string Postcode { get; }
    }
}
