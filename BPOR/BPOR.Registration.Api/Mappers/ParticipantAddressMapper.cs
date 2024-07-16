using BPOR.Domain.Entities;
using BPOR.Registration.Api.Requests.Participants;

namespace BPOR.Registration.Api.Mappers;

public static class ParticipantAddressMapper
{
    public static void Map(CreateParticipantAddressRequest source, ParticipantAddress destination)
    {
        if (source == null)
        {
            return;
        }

        destination.AddressLine1 = source.AddressLine1;
        destination.AddressLine2 = source.AddressLine2;
        destination.AddressLine3 = source.AddressLine3;
        destination.AddressLine4 = source.AddressLine4;
        destination.Town = source.Town;
        destination.Postcode = source.Postcode;
    }
}
