using Application.Responses.V1.Addresses;

namespace Application.Mappings.Locations
{
    public class AddressMapper
    {
        public static PostcodeAddressModel MapTo(Dte.Location.Api.Client.Models.PostcodeAddressModel source)
        {
            return new PostcodeAddressModel
            {
                FullAddress = source.FullAddress,
                AddressLine1 = source.AddressLine1,
                AddressLine2 = source.AddressLine2,
                AddressLine3 = source.AddressLine3,
                AddressLine4 = source.AddressLine4,
                Town = source.Town,
                Postcode = source.Postcode,
            };
        }
    }
}