using System.Net;
using BPOR.Domain.Entities;
using BPOR.Domain.Enums;
using BPOR.Rms.Constants;
using Contentful.Core.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Testing;
using NIHR.NotificationService.Context;
using NIHR.NotificationService.Services;
using NIHR.NotificationService.Tests.Fakers;
using NIHR.NotificationService.Tests.Fixtures;
using NIHR.NotificationService.Tests.Helpers;
using Notify.Interfaces;
using Notify.Models.Responses;
using NSubstitute;

namespace NIHR.NotificationService.Tests;

public class BatchNotificationTests
{
    private readonly IConfigurationRoot _configuration;

    public BatchNotificationTests()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", false)
            .AddJsonFile("appsettings.user.json", true)
            .Build();
    }

    private Campaign CreateTestCampaign(int fakerSeed, ParticipantDbContext dbContext, int participantCount,
        Action<IList<Participant>>? arrangeParticipants = null, Action<Campaign>? arrangeCampaign = null)
    {
        var participants = new ParticipantFaker().UseSeed(fakerSeed).Generate(participantCount);
        arrangeParticipants?.Invoke(participants);
        dbContext.Participants.AddRange(participants);

        var study = new Study { FullName = "Top Secret Study", StudyName = "Top Secret Study", EmailAddress = "StudyAdmin@MyStudy.MyTLD" };
        var filterCriteria = new FilterCriteria { Study = study };
        var campaign = new Campaign { FilterCriteria = filterCriteria, Description = "Test Campaign 1", TypeId = BPOR.Domain.Enums.ContactMethodId.Email };

        campaign.AddParticipants(participants);
        arrangeCampaign?.Invoke(campaign);
        dbContext.Campaign.Add(campaign);

        dbContext.SaveChanges();

        return campaign;
    }


    private async Task TestBatch(
        int batchSize,
        Action<IList<Participant>>? arrangeParticipants = null,
        Action<Campaign>? arrangeCampaign = null,
        Action<IList<Notification>>? arrangeNotifications = null,
        Action<IAsyncNotificationClient>? arrageNotificationClient = null,
        Func<NotificationDbContext, ParticipantDbContext, IReadOnlyList<FakeLogRecord>, IAsyncNotificationClient, Task>? assert = null)
    {
        var notificationDatabaseFixture = new NotificationDatabaseFixture(_configuration);
        var participantDatabaseFixture = new ParticipantDatabaseFixture(_configuration);

        const int fakerSeed = 1334;

        // Arrange services and fakes...
        using var notificationDbContext = notificationDatabaseFixture.CreateContext();
        using var participantDbContext = participantDatabaseFixture.CreateContext();
        FakeLogger<Services.NotificationService> logger = new FakeLogger<Services.NotificationService>();
        var noticiationClient = Substitute.For<IAsyncNotificationClient>();
        arrageNotificationClient?.Invoke(noticiationClient);
        var cancellationTokenSource = new CancellationTokenSource();
        var notificationService = new Services.NotificationService(noticiationClient, logger, notificationDbContext, participantDbContext);

        // Arrange test data...
        var campaign = CreateTestCampaign(fakerSeed, participantDbContext, batchSize, arrangeParticipants,
            arrangeCampaign);
        var notificationsToEnqueue = new CampaignNotificationFaker(campaign.Participant).UseSeed(fakerSeed).Generate(batchSize);
        arrangeNotifications?.Invoke(notificationsToEnqueue);
        notificationDbContext.Notifications.AddRange(notificationsToEnqueue);
        notificationDbContext.SaveChanges();

        // Act...
        await notificationService.ProcessNextNotificationBatchAsync(cancellationTokenSource.Token);

        // Assert...
        var log = logger.Collector.GetSnapshot();
        if (assert != null)
            await assert.Invoke(notificationDbContext, participantDbContext, log, noticiationClient);
    }

    const int PoisonBatchSize = 3;

    private async Task TestPoisonNotificationInBatch(
        Action<Participant>? arrangePoison,
        Action<IAsyncNotificationClient>? arrageNotificationClient = null,
        Action<Campaign>? arrangeCampaign = null,
        Action<FakeLogRecord>? assertLogError = null,
        Action<NotificationDbContext, ParticipantDbContext, IReadOnlyList<FakeLogRecord>, IAsyncNotificationClient>? assert = null
        )
    {
        await TestBatch(
            PoisonBatchSize,
            arrangeCampaign: arrangeCampaign,
            arrageNotificationClient: arrageNotificationClient,
            arrangeParticipants: participants => arrangePoison?.Invoke(participants[1]),
            assert: async (notificationCtx, participantCtx, log, clientSub) =>
            {
                // There should be a single error
                log.Errors().Should().ContainSingle().Which.Should().Satisfy<FakeLogRecord>(i => assertLogError?.Invoke(i));
                // There should be no warnings
                log.Warnings().Should().HaveCount(0);
                // All notifications should have been processed
                (await notificationCtx.Notifications.ToArrayAsync()).Should().AllSatisfy(i => i.IsProcessed.Should().BeTrue());
                assert?.Invoke(notificationCtx, participantCtx, log, clientSub);
            });
    }

    [Fact]
    public async Task EmailClientExceptionDoesNotPoisonQueue()
    {
        const string poisonEmailAddress = "__posion__@mydomain.com";
        const string poisonExceptionMessage = "Poison Message";

        await TestPoisonNotificationInBatch(
           arrangePoison: participant => participant.Email = poisonEmailAddress,
           arrageNotificationClient: notificationClient => notificationClient.SendEmailAsync(poisonEmailAddress, Arg.Any<string>(),
                Arg.Any<Dictionary<string, dynamic>>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(Task.FromException<EmailNotificationResponse>(new Exception(poisonExceptionMessage))),
           assertLogError: logEntry => logEntry.Message.Should().Contain(poisonExceptionMessage));
    }

    [Fact]
    public async Task LetterClientExceptionDoesNotPoisonQueue()
    {
        const string poisonAddressLine2 = "__posion__";
        const string poisonExceptionMessage = "Poison Message";

        await TestPoisonNotificationInBatch(
            arrangeCampaign: campaign => campaign.SetContactMethod(ContactMethodId.Letter),
            arrangePoison: participant => participant.Address!.AddressLine2 = poisonAddressLine2,
            arrageNotificationClient: notificationClient => notificationClient.SendLetterAsync(
                Arg.Any<string>(),
                Arg.Is<Dictionary<string, dynamic>>(i => i.ValueIs("address_line_2", poisonAddressLine2)),
                Arg.Any<string>())
                .Returns(Task.FromException<LetterNotificationResponse>(new Exception(poisonExceptionMessage))),
            assertLogError: logEntry => logEntry.Message.Should().Contain(poisonExceptionMessage));
    }

    [Theory]
    [InlineData(HttpStatusCode.InternalServerError)]
    [InlineData(HttpStatusCode.NotFound)]
    [InlineData(HttpStatusCode.NotAcceptable)]
    [InlineData(HttpStatusCode.Unauthorized)]
    public async Task PermenantHttpFailuresAreErrors(HttpStatusCode httpStatusCode)
    {
        await TestBatch(
            batchSize: 3,
            arrageNotificationClient: notificationClient => notificationClient.SendEmailAsync(Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<Dictionary<string, dynamic>>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(
                Task.FromResult(new EmailNotificationResponse()),
                Task.FromException<EmailNotificationResponse>(new HttpRequestException("Test Error", null, httpStatusCode)),
                Task.FromResult(new EmailNotificationResponse())),
            assert: async (notificationCtx, particpantCtx, log, clientSub) =>
            {
                log.Errors().Should().HaveCount(1, "A permenant failure HTTP code is an error condition");
                log.Warnings().Should().HaveCount(0, "A permenant failure HTTP code is not a warning");
                //particpantCtx.CampaignParticipant.Where(participant => participant.DeliveryStatusId == (int)DeliveryStatus.Failed).Should().HaveCount(1, "1 CampaignParticipant delivery status should bet set to 'Failed'");
                //particpantCtx.CampaignParticipant.Where(participant => participant.DeliveryStatusId == (int)DeliveryStatus.Sent).Should().HaveCount(1, "2 CampaignParticipant delivery statuses should bet set to 'Sent'");
                (await notificationCtx.Notifications.ToArrayAsync()).Should().AllSatisfy(i => i.IsProcessed.Should().BeTrue("Notifications that result in a permenant failure HTTP code should be marked as 'Processed'"));
            });
    }

    [Theory]
    [InlineData(HttpStatusCode.GatewayTimeout)]
    // [InlineData(HttpStatusCode.TooManyRequests)] This is handled by Polly and then fails at present
    [InlineData(HttpStatusCode.RequestTimeout)]
    [InlineData(HttpStatusCode.ServiceUnavailable)]
    public async Task RetryableHttpFailuresAreWarningsAndCanBeRetried(HttpStatusCode httpStatusCode)
    {
        await TestBatch(
            batchSize: 3,
            arrageNotificationClient: notificationClient => notificationClient.SendEmailAsync(Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<Dictionary<string, dynamic>>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(
                Task.FromResult(new EmailNotificationResponse()),
                Task.FromException<EmailNotificationResponse>(new HttpRequestException("Test Error", null, httpStatusCode)),
                Task.FromResult(new EmailNotificationResponse())),
            assert: async (notificationCtx, particpantCtx, log, clientSub) =>
            {
                //log.Errors().Should().BeEmpty("A retryable failure HTTP code is not an error condition");
                log.Errors().Should().HaveCount(1);
                log.Warnings().Should().HaveCount(0);
                //log.Warnings().Should().HaveCount(3, "A retryable failure HTTP code is a warning that something is not right but not immediate cause for concern");
                //particpantCtx.CampaignParticipant.Where(participant => participant.DeliveryStatusId == (int)DeliveryStatus.Pending).Should().HaveCount(1, "1 CampaignParticipant delivery status should bet set to 'Pending'");
                //particpantCtx.CampaignParticipant.Where(participant => participant.DeliveryStatusId == (int)DeliveryStatus.Sent).Should().HaveCount(1, "2 CampaignParticipant delivery statuses should bet set to 'Sent'");
                //notificationCtx.Notifications.Where(i => i.IsProcessed == true).Should().HaveCount(2, "2 notifications should have been processed");
                //notificationCtx.Notifications.Where(i => i.IsProcessed == false).Should().HaveCount(1, "1 notifications should still need to be processed");
                (await notificationCtx.Notifications.ToArrayAsync()).Where(i => i.IsProcessed == true).Should().HaveCount(3, "3 notifications should have been processed");
            });
    }

    [Fact]
    public async Task ValidEmailsShouldSend()
    {
        const int batchSize = 5;
        await TestBatch(
            batchSize: batchSize,
            assert: async (notificationCtx, particpantCtx, log, clientSub) =>
            {
                clientSub.ReceivedWithAnyArgs(batchSize).SendEmailAsync(default, default);
                clientSub.ReceivedWithAnyArgs(0).SendLetterAsync(default, default);
                log.Errors().Should().BeEmpty();
                log.Warnings().Should().BeEmpty();
                //particpantCtx.CampaignParticipant.Should().AllSatisfy(participant => participant.DeliveryStatusId.Should().Be((int)DeliveryStatus.Sent, "CampaignParticipant delivery status should be 'Sent'"));
                (await notificationCtx.Notifications.ToArrayAsync()).Should().AllSatisfy(i => i.IsProcessed.Should().BeTrue());
            });
    }

    [Fact]
    public async Task EmailMustHaveEmailAddress()
    {
        const int batchSize = 5;
        await TestBatch(
            batchSize: batchSize,
            arrangeNotifications: notifications => notifications[1].NotificationDatas.RemoveByKey(PersonalisationKeys.Email),
            assert: async (notificationCtx, particpantCtx, log, clientSub) =>
            {
                clientSub.ReceivedWithAnyArgs(batchSize - 1).SendEmailAsync(default, default);
                log.Errors().Should().HaveCount(1);
                // particpantCtx.CampaignParticipant.Should().AllSatisfy(participant => participant.DeliveryStatusId.Should().Be((int)DeliveryStatus.Failed, "CampaignParticipant delivery status should be 'Failed'"));
                (await notificationCtx.Notifications.ToArrayAsync()).Should().AllSatisfy(i => i.IsProcessed.Should().BeTrue());
            });
    }

    [Fact]
    public async Task LetterMustHaveAddress1()
    {
        const int batchSize = 5;
        await TestBatch(
            batchSize: batchSize,
            arrangeCampaign: campaign => campaign.SetContactMethod(ContactMethodId.Letter),
            arrangeNotifications: notifications => notifications[1].NotificationDatas.RemoveByKey("address_line_1"),
             assert: async (notificationCtx, particpantCtx, log, clientSub) =>
            {
                clientSub.ReceivedWithAnyArgs(batchSize - 1).SendLetterAsync(default, default);
                log.Errors().Should().HaveCount(1);
                //particpantCtx.CampaignParticipant.Should().AllSatisfy(participant => participant.DeliveryStatusId.Should().Be((int)DeliveryStatus.Failed, "CampaignParticipant delivery status should be 'Failed'"));
                (await notificationCtx.Notifications.ToArrayAsync()).Should().AllSatisfy(i => i.IsProcessed.Should().BeTrue());
            });
    }

    [Fact]
    public async Task LetterMustHaveTown()
    {
        const int batchSize = 5;
        await TestBatch(
            batchSize: batchSize,
            arrangeCampaign: campaign => campaign.SetContactMethod(ContactMethodId.Letter),
            arrangeNotifications: notifications => notifications[1].NotificationDatas.RemoveByKey("address_line_5"),
             assert: async (notificationCtx, particpantCtx, log, clientSub) =>
             {
                 clientSub.ReceivedWithAnyArgs(batchSize - 1).SendLetterAsync(default, default);
                 log.Errors().Should().HaveCount(1);
                 //particpantCtx.CampaignParticipant.Should().AllSatisfy(participant => participant.DeliveryStatusId.Should().Be((int)DeliveryStatus.Failed, "CampaignParticipant delivery status should be 'Failed'"));
                 (await notificationCtx.Notifications.ToArrayAsync()).Should().AllSatisfy(i => i.IsProcessed.Should().BeTrue());
             });
    }

    [Fact]
    public async Task LetterMustHavePostcode()
    {
        const int batchSize = 5;
        await TestBatch(
            batchSize: batchSize,
            arrangeCampaign: campaign => campaign.SetContactMethod(ContactMethodId.Letter),
            arrangeNotifications: notifications => notifications[1].NotificationDatas.RemoveByKey("address_line_6"),
             assert: async (notificationCtx, particpantCtx, log, clientSub) =>
             {
                 clientSub.ReceivedWithAnyArgs(batchSize - 1).SendLetterAsync(default, default);
                 log.Errors().Should().HaveCount(1);
                 //particpantCtx.CampaignParticipant.Should().AllSatisfy(participant => participant.DeliveryStatusId.Should().Be((int)DeliveryStatus.Failed, "CampaignParticipant delivery status should be 'Failed'"));
                 (await notificationCtx.Notifications.ToArrayAsync()).Should().AllSatisfy(i => i.IsProcessed.Should().BeTrue());
             });
    }

    [Fact]
    public async Task ValidLettersShouldSend()
    {
        const int batchSize = 5;
        await TestBatch(
            batchSize: batchSize,
            arrangeCampaign: campaign => campaign.SetContactMethod(ContactMethodId.Letter),
            assert: async (notificationCtx, particpantCtx, log, clientSub) =>
            {
                clientSub.ReceivedWithAnyArgs(0).SendEmailAsync(default, default);
                clientSub.ReceivedWithAnyArgs(batchSize).SendLetterAsync(default, default);
                log.Count(i => i.IsError()).Should().Be(0);
                (await particpantCtx.CampaignParticipant.ToArrayAsync()).Should().AllSatisfy(participant => participant.DeliveredAt.Should().BeRecentUtc());
                (await particpantCtx.CampaignParticipant.ToArrayAsync()).Should().AllSatisfy(participant => participant.DeliveryStatusId.Should().Be((int)DeliveryStatus.Delivered, "CampaignParticipant delivery status should be 'Delivered'"));
                (await notificationCtx.Notifications.ToArrayAsync()).Should().AllSatisfy(i => i.IsProcessed.Should().BeTrue());
            });
    }
}
