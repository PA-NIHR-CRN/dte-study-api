using DYNAMO.STREAM.HANDLER.Entities;
using Newtonsoft.Json;

namespace DYNAMO.STREAM.HANDLER.Mappers;

public static class ParticipantAddressMapper
{
    public static ParticipantAddress Map(string address)
    {
        var source = JsonConvert.DeserializeObject<ParticipantAddress>(address);

        return new ParticipantAddress
        {
            AddressLine1 = source.AddressLine1,
            AddressLine2 = source.AddressLine2,
            AddressLine3 = source.AddressLine3,
            AddressLine4 = source.AddressLine4,
            Town = source.Town,
            Postcode = source.Postcode,
        };
    }
}
