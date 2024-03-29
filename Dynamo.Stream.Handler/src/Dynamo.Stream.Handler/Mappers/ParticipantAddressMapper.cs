using Dynamo.Stream.Handler.Entities;

namespace Dynamo.Stream.Handler.Mappers;

public static class ParticipantAddressMapper
{
    public static ParticipantAddress? Map(Domain.Entities.Participants.ParticipantAddress? source, Dynamo.Stream.Handler.Entities.Participant? destination)
    {
        if (source != null && destination?.Address == null)
        {
            destination.Address = new ParticipantAddress();
        }

        if (source != null && destination != null)
        {
            destination.Address.AddressLine1 = source.AddressLine1;
            destination.Address.AddressLine2 = source.AddressLine2;
            destination.Address.AddressLine3 = source.AddressLine3;
            destination.Address.AddressLine4 = source.AddressLine4;
            destination.Address.Postcode = source.Postcode;
            destination.Address.Town = source.Town;
        }

        return destination.Address;
    }
}
