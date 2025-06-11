using System.Diagnostics;
using Microsoft.Extensions.Logging;
using NIHR.NotificationService.Context;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using Notify.Client;
using Notify.Models.Responses;
using Polly;
using Polly.RateLimit;
using BPOR.Domain.Enums;
using BPOR.Rms.Constants;
using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Notify.Interfaces;
using System.Collections.Concurrent;

namespace NIHR.NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IAsyncNotificationClient _client;
        private readonly ILogger<NotificationService> _logger;
        private readonly NotificationDbContext _notificationDbContext;
        private readonly ParticipantDbContext _participantDbContext;
        private const int _rateLimitPerMinute = 3000;
        private static readonly Dictionary<ContactMethodId, int> _dailyCount = new();
        private static readonly Dictionary<ContactMethodId, int> _dailyLimit = new()
        {
            { ContactMethodId.Email, 250000 },
            { ContactMethodId.Letter, 20000 }
        };

        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public NotificationService(IAsyncNotificationClient client, ILogger<NotificationService> logger,
            NotificationDbContext notificationDbContext, ParticipantDbContext participantDbContext)
        {
            _client = client;
            _logger = logger;
            _notificationDbContext = notificationDbContext;
            _participantDbContext = participantDbContext;

            foreach (var method in Enum.GetValues<ContactMethodId>())
            {
                _dailyCount[method] = 0;
            }
        }

        public async Task ProcessNextNotificationBatchAsync(CancellationToken stoppingToken)
        {
            var notifications = await _notificationDbContext.Notifications
                .Where(n => !n.IsProcessed)
                .OrderBy(n => n.Id)
                .Take(1000).Include(n => n.NotificationDatas)
                .ToListAsync(stoppingToken);

            if (notifications.Count > 0)
            {
                _logger.LogInformation("Processing {NotificationsCount} notifications", notifications.Count);

                try
                {
                    var successfullySentNotifications = new ConcurrentBag<Notification>();
                    await SendBatchNotificationAsync(notifications, successfullySentNotifications, stoppingToken);

                    foreach(Notification notification in notifications)
                    {
                        notification.IsProcessed = true;
                    }


                    foreach (Notification notification in successfullySentNotifications)
                    {
                        await IfLetterThenUpdateDeliveryStatusAsync(notification);
                    }

                    await _notificationDbContext.SaveChangesAsync(stoppingToken);
                    await _participantDbContext.SaveChangesAsync(stoppingToken);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while sending notification emails");
                }
            }
        }

        private async Task IfLetterThenUpdateDeliveryStatusAsync(Notification notification)
        {
            var personalisation = notification.NotificationDatas.ToDictionary(x => x.Key, x => x.Value);

            if (!personalisation.TryGetValue(PersonalisationKeys.CampaignTypeId, out var ctValue)
                || !int.TryParse(ctValue, out var campaignType)
                || campaignType != (int)ContactMethodId.Letter)
            {
                return;
            }

            if (!personalisation.TryGetValue(PersonalisationKeys.CampaignParticipantId, out var campaignParticipantIdValue)
                || !int.TryParse(campaignParticipantIdValue, out var campaignParticipantId))
            {
                _logger.LogWarning(
                    "Notification ID {NotificationId}: Missing or invalid campaignParticipantId: '{CampaignParticipantIdValue}'",
                    notification.Id,
                    campaignParticipantIdValue);
                return;
            }

            var participant = await _participantDbContext.CampaignParticipant.FirstOrDefaultAsync(x => x.Id == campaignParticipantId);
            if (participant == null)
            {
                _logger.LogWarning("Notification ID {NotificationId}: Participant ID {ParticipantId} not found", notification.Id, campaignParticipantId);
                return;
            }

            participant.DeliveredAt = DateTime.UtcNow;
            participant.DeliveryStatusId = (int)DeliveryStatus.Delivered;
        }

        public async Task SendNotificationAsync(SendNotificationRequest request, CancellationToken cancellationToken)
        {
            request.Validate();

            var stopwatch = Stopwatch.StartNew();
            try
            {
                var personalisation = request.Personalisation.ToDictionary(x => x.Key, x => (dynamic)x.Value);

                var contactMethod = (ContactMethodId)int.Parse(personalisation[PersonalisationKeys.CampaignTypeId]);

                switch (contactMethod)
                {
                    case ContactMethodId.Email:
                        await _client.SendEmailAsync(request.EmailAddress, request.TemplateId, personalisation, request.Reference);
                        await IncrementDailyCountAsync(ContactMethodId.Email, 1);
                        break;

                    case ContactMethodId.Letter:
                        var letterResponse = await _client.SendLetterAsync(request.TemplateId, personalisation, request.Reference);
                        await IncrementDailyCountAsync(ContactMethodId.Letter, 1);
                        break;

                    default:
                        throw new NotSupportedException($"Contact method {contactMethod} is not supported.");
                }
            }
            catch (HttpRequestException httpEx) when (httpEx.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                _logger.LogError(httpEx, "429 Rate Limit Exceeded error while sending notification");
                throw new InvalidOperationException("429 Rate Limit Exceeded error while sending notification.", httpEx);
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation("Request for {Reference} took {ElapsedMilliseconds} ms", request.Reference, stopwatch.ElapsedMilliseconds);
            }
        }

        public async Task SendPreviewEmailAsync(SendNotificationRequest request, CancellationToken cancellationToken)
        {
            var personalisation = request.Personalisation.ToDictionary(x => x.Key, x => (dynamic)x.Value);

            await _client.SendEmailAsync(request.EmailAddress, request.TemplateId,
                personalisation, request.Reference);
        }

        public async Task<NotificationResponse> SendBatchNotificationAsync(List<Notification> notifications, ConcurrentBag<Notification> successfullySentNotifications,
            CancellationToken cancellationToken)
        {
            const int batchSize = 100;

            var rateLimitPolicy = Policy.RateLimitAsync(_rateLimitPerMinute, TimeSpan.FromMinutes(1));
            var retryPolicy = Policy
                .Handle<HttpRequestException>(ex => ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(20));

            var tasks = new List<Task>();
            var batches = notifications
                .Select((notification, index) => new { Index = index, Value = notification })
                .GroupBy(item => item.Index / batchSize, item => item.Value);

            var totalStopwatch = Stopwatch.StartNew();

            foreach (var batch in batches)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Batch notification sending cancelled");
                    cancellationToken.ThrowIfCancellationRequested();
                }

                tasks.Add(SendBatchWithRateLimitAsync(batch.ToList(), rateLimitPolicy, retryPolicy, successfullySentNotifications, cancellationToken));
            }

            await Task.WhenAll(tasks);

            totalStopwatch.Stop();
            _logger.LogInformation("Total time for all batches: {TotalMilliseconds} ms", totalStopwatch.ElapsedMilliseconds);

            return new NotificationResponse();
        }

        private async Task SendBatchWithRateLimitAsync(List<Notification> batch,
            AsyncRateLimitPolicy rateLimitPolicy, AsyncPolicy retryPolicy, ConcurrentBag<Notification> successfullySentNotifications, CancellationToken cancellationToken)
        {
            var batchStopwatch = Stopwatch.StartNew();
            var individualTimes = new List<long>();

            var tasks = batch.Select(notification =>
                SendNotificationWithRetryAsync(notification, retryPolicy, successfullySentNotifications, cancellationToken, individualTimes)).ToList();

            await rateLimitPolicy.ExecuteAsync(() => Task.WhenAll(tasks));

            batchStopwatch.Stop();
            var averageTimePerRequest = individualTimes.Average();
            _logger.LogInformation("Batch of {BatchCount} notifications took {ElapsedMilliseconds} ms. Average time per request: {AverageTimePerRequest} ms",
                batch.Count, batchStopwatch.ElapsedMilliseconds, averageTimePerRequest);
        }

        private async Task SendNotificationWithRetryAsync(Notification notification,
            AsyncPolicy retryPolicy, ConcurrentBag<Notification> successfullySentNotifications, CancellationToken cancellationToken, List<long> individualTimes)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            { 
                var personalisation = notification.NotificationDatas.ToDictionary(x => x.Key, x => x.Value);
                var sendNotificationRequest = CreateSendNotificationRequest(personalisation);
                await retryPolicy.ExecuteAsync(async () => { await SendNotificationAsync(sendNotificationRequest, cancellationToken); });
                successfullySentNotifications.Add(notification);
            }
            catch (Exception ex)
            {
                // Do not rethrow as this will cause the entire batch to fail.
                _logger.LogError(ex, "Unable to send notification ID:{NotificaitionId}; {Reason}", notification.Id, ex.Message);
            }

            stopwatch.Stop();
            lock (individualTimes)
            {
                individualTimes.Add(stopwatch.ElapsedMilliseconds);
            }
        }

        private static SendNotificationRequest CreateSendNotificationRequest(Dictionary<string, string> personalisation)
        {
            if (!personalisation.TryGetValue(PersonalisationKeys.CampaignParticipantId, out var reference))
            {
                throw new KeyNotFoundException("campaignParticipantId not found in personalisation data.");
            }

            if (!personalisation.TryGetValue(PersonalisationKeys.TemplateId, out var templateId))
            {
                throw new KeyNotFoundException("templateId not found in personalisation data.");
            }

            if (!personalisation.TryGetValue(PersonalisationKeys.CampaignTypeId, out var campaignTypeIdStr))
            {
                throw new KeyNotFoundException("campaignTypeId not found in personalisation data.");
            }

            if (!int.TryParse(campaignTypeIdStr, out var campaignTypeId))
            {
                throw new ArgumentException($"campaignTypeId '{campaignTypeIdStr}' is not a valid integer.");
            }

            var request = new SendNotificationRequest
            {
                TemplateId = templateId,
                Personalisation = personalisation,
                Reference = reference,
                ContactMethod = (ContactMethodId)campaignTypeId
            };

            var contactMethod = (ContactMethodId)int.Parse(personalisation[PersonalisationKeys.CampaignTypeId]);

            switch (contactMethod)
            {
                case ContactMethodId.Email:
                    if (!request.Personalisation.TryGetValue("email", out var email) || string.IsNullOrWhiteSpace(email))
                    {
                        throw new ArgumentException("EmailAddress is required in personalisation for email notifications.");
                    }
                    request.EmailAddress = email;
                    break;

                case ContactMethodId.Letter:
                    if (!personalisation.TryGetValue("address_line_1", out var addressLine1) ||
                        !personalisation.TryGetValue("address_line_5", out var town) ||
                        !personalisation.TryGetValue("address_line_6", out var postcode))
                    {
                        throw new KeyNotFoundException("Letter notifications require at least 3 address lines");
                    }

                    request.Personalisation["address_line_1"] = addressLine1;

                    for (int i = 2; i <= 4; i++) // assign optional address fields
                    {
                        var key = $"address_line_{i}";
                        if (personalisation.TryGetValue(key, out var value))
                        {
                            request.Personalisation[key] = value;
                        }
                    }

                    request.Personalisation["address_line_5"] = town;
                    request.Personalisation["address_line_6"] = postcode;
                    request.Personalisation["address_postcode"] = postcode;
                    break;

                default:
                    throw new NotSupportedException($"Contact method {contactMethod} is not supported.");
            }

            return request;
        }

        private async Task IncrementDailyCountAsync(ContactMethodId contactMethod, int count)
        {
            var lockStopwatch = Stopwatch.StartNew();
            await _semaphore.WaitAsync();
            lockStopwatch.Stop();

            _logger.LogInformation("Waited {ElapsedMilliseconds} ms for semaphore lock", lockStopwatch.ElapsedMilliseconds);

            try
            {
                if (!_dailyCount.ContainsKey(contactMethod))
                {
                    throw new InvalidOperationException($"Unsupported contact method: {contactMethod}");
                }

                _dailyCount[contactMethod] += count;

                if (_dailyCount[contactMethod] >= _dailyLimit[contactMethod])
                {
                    throw new InvalidOperationException($"Daily limit reached for {contactMethod}: {_dailyLimit[contactMethod]}");
                }
            }
            finally
            {
                _semaphore.Release();
                _logger.LogInformation("Lock duration for IncrementDailyCountAsync: {ElapsedMilliseconds} ms", lockStopwatch.ElapsedMilliseconds);
            }
        }


        public async Task<TemplateList> GetTemplatesAsync(CancellationToken cancellationToken)
        {
            return await _client.GetAllTemplatesAsync();
        }
    }
}
