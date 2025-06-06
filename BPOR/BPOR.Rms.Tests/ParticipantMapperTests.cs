using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Mappers;
using Microsoft.Extensions.Logging.Testing;
using NIHR.Infrastructure;
using NSubstitute;
using PostcodeAddressModel = NIHR.Infrastructure.Models.PostcodeAddressModel;

namespace BPOR.Rms.Tests
{
    public class ParticipantMapperTests
    {
        [Fact]
        public async Task IdenticalPostcodesDoesNotResultInLocationLookup()
        {
            // Arrange
            var logger = new FakeLogger<ParticipantMapper>();
            var locationService = Substitute.For<IPostcodeMapper>();
            var participantMapper = new ParticipantMapper(null, null, locationService, logger);
            var pariticipant = new Participant { Address = new ParticipantAddress { Postcode = "BT30 9GR" } };
            var dynamoParticipantAddress = new DynamoParticipantAddress { Postcode = "BT30 9GR" };

            // Act
            await participantMapper.MapAddress(dynamoParticipantAddress, pariticipant, CancellationToken.None);

            // Assert
            locationService.DidNotReceiveWithAnyArgs().GetAddressesByPostcodeAsync(default, default);
            locationService.DidNotReceiveWithAnyArgs().GetCoordinatesFromPostcodeAsync(default, default);
        }

        [Fact]
        public async Task InsignificantPostcodeChangesDoesNotResultInLocationLookup()
        {
            // Arrange
            var logger = new FakeLogger<ParticipantMapper>();
            var locationService = Substitute.For<IPostcodeMapper>();
            var participantMapper = new ParticipantMapper(null, null, locationService, logger);
            var pariticipant = new Participant { Address = new ParticipantAddress { Postcode = "BT30 9GR" } };
            var dynamoParticipantAddress = new DynamoParticipantAddress { Postcode = "BT309GR" };

            // Act
            await participantMapper.MapAddress(dynamoParticipantAddress, pariticipant, CancellationToken.None);

            // Assert
            locationService.DidNotReceiveWithAnyArgs().GetAddressesByPostcodeAsync(default, default);
            locationService.DidNotReceiveWithAnyArgs().GetCoordinatesFromPostcodeAsync(default, default);
        }

        [Fact]
        public async Task SignificantlyDifferingPostcodesResultsInLocationLookup()
        {
            // Arrange
            var logger = new FakeLogger<ParticipantMapper>();
            var locationService = Substitute.For<IPostcodeMapper>();
            var participantMapper = new ParticipantMapper(null, null, locationService, logger);
            var pariticipant = new Participant { Address = new ParticipantAddress { Postcode = "BT30 9HR" } };
            var dynamoParticipantAddress = new DynamoParticipantAddress { Postcode = "BT30 9GR" };

            // Act
            await participantMapper.MapAddress(dynamoParticipantAddress, pariticipant, CancellationToken.None);

            // Assert
            locationService.ReceivedWithAnyArgs().GetAddressesByPostcodeAsync(default, default);
            locationService.ReceivedWithAnyArgs().GetCoordinatesFromPostcodeAsync(default, default);
        }

        [Fact]
        public async Task CanonicalTownFromLocationServiceIsStoredInParticipant()
        {
            // Arrange
            var logger = new FakeLogger<ParticipantMapper>();
            var locationService = Substitute.For<IPostcodeMapper>();
            locationService.GetAddressesByPostcodeAsync("BT30 9GR", Arg.Any<CancellationToken>()).Returns([new PostcodeAddressModel { Town = "Downpatrick" }]);
            var participantMapper = new ParticipantMapper(null, null, locationService, logger);
            var pariticipant = new Participant { Address = new ParticipantAddress { Postcode = "BT30 8HR" } };
            var dynamoParticipantAddress = new DynamoParticipantAddress { Postcode = "BT30 9GR" };

            // Act
            await participantMapper.MapAddress(dynamoParticipantAddress, pariticipant, CancellationToken.None);

            // Assert
            Assert.Equal("Downpatrick", pariticipant.Address.CanonicalTown);
        }
    }
}
