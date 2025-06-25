using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.DynamoDBEvents;
using AWS.Lambda.Powertools.Idempotency;
using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Handlers;
using BPOR.Registration.Stream.Handler.Mappers;
using BPOR.Registration.Stream.Handler.Services;
using BPOR.Tests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging.Testing;
using NIHR.Infrastructure;
using NIHR.Infrastructure.Models;
using NSubstitute;
using Xunit;

namespace BPOR.Registration.Stream.Handler.Tests
{
    public class IdempotencyTests
    {
        [Fact]
        public async Task ExecuteAAndBThenCResultsInSingleParticipant()
        {
            // Arrange
            var interceptor = new SaveChangeCountInterceptor();
            using var participantDbContext = new ParticipantDbContext(new DbContextOptionsBuilder<ParticipantDbContext>()
                .UseInMemoryDatabase("IdempotencyTests")
                .AddInterceptors(interceptor)
                .Options
                );

            // Mock a location service with a 1 second delay - this allows us to test for concurrent operations by executing two calls to the Lambda with a 500ms interval.
            var locationService = Substitute.For<IPostcodeMapper>();
            locationService.GetAddressesByPostcodeAsync(default, default).ReturnsForAnyArgs(async callInfo => { await Task.Delay(1000); return [new PostcodeAddressModel { Town = "Blandford Forum", Postcode = "DT11 9HG" }]; });
            locationService.GetCoordinatesFromPostcodeAsync(default, default).ReturnsForAnyArgs(async callInfo => { await Task.Delay(1000); return new CoordinatesModel { Latitude = 50.823885, Longitude = -2.1265653 }; });

            Idempotency.Configure(builder => builder.UseInMemoryDb());

            var dynamoDbContext = new DynamoDBContext(Substitute.For<IAmazonDynamoDB>());
            var refDataService = new RefDataService(participantDbContext, new FakeLogger<RefDataService>());
            var participantMapper = new ParticipantMapper(dynamoDbContext, refDataService, locationService, new FakeLogger<ParticipantMapper>());
            var streamHandler = new StreamHandler(participantDbContext, new FakeLogger<StreamHandler>(), participantMapper);

            var participantFaker = new DynamoParticipantFaker();
            participantFaker.UseSeed(49937);
            DynamoParticipant dynamoParticipant = participantFaker.Generate();

            var eventIdGenerator = new Bogus.Randomizer(62841);
            var dynamoDbEvent = new DynamoDBEvent { Records = [dynamoDbContext.CreateInsertStreamRecord(dynamoParticipant, eventIdGenerator.Guid().ToString())] };

            // Act

            var call_1 = streamHandler.ProcessStreamAsync(dynamoDbEvent, CancellationToken.None);
            await Task.Delay(500);
            var failures_2 = await streamHandler.ProcessStreamAsync(dynamoDbEvent, CancellationToken.None);
            var failures_1 = await call_1;
            await Task.Delay(500);
            var failures_3 = await streamHandler.ProcessStreamAsync(dynamoDbEvent, CancellationToken.None);

            // Assert

            failures_1.Should().BeEmpty();
            failures_2.Should().HaveCount(1);
            failures_3.Should().BeEmpty();
            // Check the number of calls to SaveChangesAsync to check that the stream handler only ran once
            interceptor.SaveChancesAsyncCount.Should().Be(1);
        }
    }
}
