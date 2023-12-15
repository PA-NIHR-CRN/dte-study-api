using Dynamo.Stream.Handler.Mappers;

namespace Dynamo.Stream.Handler.Tests.Mappers;

public class ParticipantAddressMapperTests
{
    [Fact]
    public void Map_ReturnsMappedParticipantAddress_WhenSourceIsNotNull()
    {
        // Arrange
        var source = new Domain.Entities.Participants.ParticipantAddress
        {
            AddressLine1 = "AddressLine1",
            AddressLine2 = "AddressLine2",
            AddressLine3 = "AddressLine3",
            AddressLine4 = "AddressLine4",
            Town = "Town",
            Postcode = "Postcode"
        };
        var participantId = 1;

        // Act
        var result = ParticipantAddressMapper.Map(source, participantId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(source.AddressLine1, result.AddressLine1);
        Assert.Equal(source.AddressLine2, result.AddressLine2);
        Assert.Equal(source.AddressLine3, result.AddressLine3);
        Assert.Equal(source.AddressLine4, result.AddressLine4);
        Assert.Equal(source.Town, result.Town);
        Assert.Equal(source.Postcode, result.Postcode);
        Assert.Equal(participantId, result.ParticipantId);
    }

    [Fact]
    public void Map_ReturnsNull_WhenSourceIsNull()
    {
        // Arrange
        Domain.Entities.Participants.ParticipantAddress? source = null;
        var participantId = 1;

        // Act
        var result = ParticipantAddressMapper.Map(source, participantId);

        // Assert
        Assert.Null(result);
    }
}
