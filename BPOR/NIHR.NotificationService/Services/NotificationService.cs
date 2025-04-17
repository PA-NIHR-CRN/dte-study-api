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

namespace NIHR.NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationClient _client;
        private readonly ILogger<NotificationService> _logger;
        private const int _rateLimitPerMinute = 3000;

        private static readonly Dictionary<ContactMethodId, int> _dailyCount = new();
        private static readonly Dictionary<ContactMethodId, int> _dailyLimit = new()
        {
            { ContactMethodId.Email, 250000 },
            { ContactMethodId.Letter, 20000 }
        };

        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public NotificationService(NotificationClient client, ILogger<NotificationService> logger)
        {
            _client = client;
            _logger = logger;

            foreach (var method in Enum.GetValues<ContactMethodId>())
            {
                _dailyCount[method] = 0;
            }
        }

        public async Task SendNotificationAsync(SendNotificationRequest request, CancellationToken cancellationToken)
        {
            request.Validate();

            var stopwatch = Stopwatch.StartNew();
            try
            {
                var personalisation = request.Personalisation.ToDictionary(x => x.Key, x => (dynamic)x.Value);

                var contactMethod = (ContactMethodId)int.Parse(personalisation["campaignTypeId"]);

                switch (contactMethod)
                {
                    case ContactMethodId.Email:
                        await _client.SendEmailAsync(request.EmailAddress, request.TemplateId, personalisation, request.Reference);
                        await IncrementDailyCountAsync(ContactMethodId.Email, 1);
                        break;

                    case ContactMethodId.Letter:
                        await _client.SendLetterAsync(request.TemplateId, personalisation, request.Reference);
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

        public async Task<NotificationResponse> SendBatchNotificationAsync(List<Notification> notifications,
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

                tasks.Add(SendBatchWithRateLimitAsync(batch.ToList(), rateLimitPolicy, retryPolicy, cancellationToken));
            }

            await Task.WhenAll(tasks);

            totalStopwatch.Stop();
            _logger.LogInformation("Total time for all batches: {TotalMilliseconds} ms", totalStopwatch.ElapsedMilliseconds);

            return new NotificationResponse();
        }

        private async Task SendBatchWithRateLimitAsync(List<Notification> batch,
            AsyncRateLimitPolicy rateLimitPolicy, AsyncPolicy retryPolicy, CancellationToken cancellationToken)
        {
            var batchStopwatch = Stopwatch.StartNew();
            var individualTimes = new List<long>();

            var tasks = batch.Select(notification =>
                SendNotificationWithRetryAsync(notification, retryPolicy, cancellationToken, individualTimes)).ToList();

            await rateLimitPolicy.ExecuteAsync(() => Task.WhenAll(tasks));

            batchStopwatch.Stop();
            var averageTimePerRequest = individualTimes.Average();
            _logger.LogInformation("Batch of {BatchCount} notifications took {ElapsedMilliseconds} ms. Average time per request: {AverageTimePerRequest} ms",
                batch.Count, batchStopwatch.ElapsedMilliseconds, averageTimePerRequest);
        }

        private async Task SendNotificationWithRetryAsync(Notification notification,
            AsyncPolicy retryPolicy, CancellationToken cancellationToken, List<long> individualTimes)
        {
            var personalisation = notification.NotificationDatas.ToDictionary(x => x.Key, x => x.Value);

            var sendNotificationRequest = CreateSendNotificationRequest(personalisation);

            var stopwatch = Stopwatch.StartNew();
            await retryPolicy.ExecuteAsync(async () => { await SendNotificationAsync(sendNotificationRequest, cancellationToken); });
            stopwatch.Stop();

            lock (individualTimes)
            {
                individualTimes.Add(stopwatch.ElapsedMilliseconds);
            }
        }

        private static SendNotificationRequest CreateSendNotificationRequest(Dictionary<string, string> personalisation)
        {
            if (!personalisation.TryGetValue("campaignParticipantId", out var reference))
            {
                throw new KeyNotFoundException("campaignParticipantId not found in personalisation data.");
            }

            if (!personalisation.TryGetValue("templateId", out var templateId))
            {
                throw new KeyNotFoundException("templateId not found in personalisation data.");
            }

            if (!personalisation.TryGetValue("campaignTypeId", out var campaignTypeIdStr))
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

            var contactMethod = (ContactMethodId)int.Parse(personalisation["campaignTypeId"]);

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
