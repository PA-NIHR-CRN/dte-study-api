using Dynamo.Stream.Handler.Entities;

namespace Dynamo.Stream.Handler.Mappers;

public static class ParticipantAddressMapper
{
    public static ParticipantAddress? Map(Domain.Entities.Participants.ParticipantAddress? source, int participantId)
    {
        return source == null
            ? null
            : new ParticipantAddress
            {
                AddressLine1 = source.AddressLine1,
                AddressLine2 = source.AddressLine2,
                AddressLine3 = source.AddressLine3,
                AddressLine4 = source.AddressLine4,
                Town = source.Town,
                Postcode = source.Postcode,
                ParticipantId = participantId
            };
    }
}
