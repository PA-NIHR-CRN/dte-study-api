using Microsoft.Extensions.Logging;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using Notify.Client;
using Notify.Models.Responses;
using Polly;
using Polly.RateLimit;

namespace NIHR.NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationClient _client;
        private readonly ILogger<NotificationService> _logger;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);
        private static int _dailyEmailCount = 0;
        private const int _dailyLimit = 250000;
        private const int _rateLimitPerMinute = 3000;

        public NotificationService(NotificationClient client, ILogger<NotificationService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task SendEmailAsync(SendEmailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _client.SendEmailAsync(request.EmailAddress, request.EmailTemplateId.ToString(),
                    request.Personalisation, request.Reference);
                await IncrementDailyEmailCountAsync(1);
            }
            catch (HttpRequestException httpEx) when (httpEx.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                _logger.LogError(httpEx, "429 Rate Limit Exceeded error while sending email");

                throw new InvalidOperationException("429 Rate Limit Exceeded error while sending email.", httpEx);
            }
        }

        public async Task<EmailNotificationResponse> SendBatchEmailAsync(SendBatchEmailRequest request,
            CancellationToken cancellationToken)
        {
            const int batchSize = 100;

            var rateLimitPolicy = Policy.RateLimitAsync(_rateLimitPerMinute, TimeSpan.FromMinutes(1));
            var retryPolicy = Policy
                .Handle<HttpRequestException>(ex => ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(20));

            var tasks = new List<Task>();

            var batches = request.EmailAddresses
                .Select((email, index) => new { email, index })
                .GroupBy(x => x.index / batchSize)
                .Select(g => g.Select(x => x.email).ToList())
                .ToList();


            foreach (var batch in batches)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Batch email sending cancelled");
                    cancellationToken.ThrowIfCancellationRequested();
                }
                
                tasks.Add(SendBatchWithRateLimitAsync(batch, request, rateLimitPolicy, retryPolicy, cancellationToken));
            }

            await Task.WhenAll(tasks);

            return new EmailNotificationResponse();
        }

        private async Task SendBatchWithRateLimitAsync(List<string> batch, SendBatchEmailRequest request,
            AsyncRateLimitPolicy rateLimitPolicy, AsyncPolicy retryPolicy, CancellationToken cancellationToken)
        {
            var tasks = batch.Select(email => SendEmailWithRetryAsync(email, request, retryPolicy, cancellationToken)).ToList();
            await rateLimitPolicy.ExecuteAsync(() => Task.WhenAll(tasks));
        }

        private async Task SendEmailWithRetryAsync(string email, SendBatchEmailRequest request,
            AsyncPolicy retryPolicy, CancellationToken cancellationToken)
        {
            var sendEmailRequest = CreateSendEmailRequest(email, request);

            await retryPolicy.ExecuteAsync(async () =>
            {
                await SendEmailAsync(sendEmailRequest, cancellationToken);
            });
        }

        private static SendEmailRequest CreateSendEmailRequest(string email, SendBatchEmailRequest request)
        {
            if (!request.PersonalisationData.TryGetValue(email, out var personalisation))
            {
                throw new KeyNotFoundException($"Personalisation data not found for email: {email}");
            }

            if (!personalisation.TryGetValue("emailCampaignParticipantId", out var reference))
            {
                throw new KeyNotFoundException($"EmailCampaignParticipantId not found for email: {email}");
            }

            return new SendEmailRequest
            {
                EmailAddress = email,
                EmailTemplateId = request.EmailTemplateId ??
                                  throw new ArgumentNullException(nameof(request.EmailTemplateId)),
                Personalisation = personalisation,
                Reference = reference.ToString()
            };
        }

        private static async Task IncrementDailyEmailCountAsync(int count)
        {
            await _semaphore.WaitAsync();
            try
            {
                _dailyEmailCount += count;
                if (_dailyEmailCount >= _dailyLimit)
                {
                    throw new InvalidOperationException("Daily email limit reached.");
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<TemplateList> GetTemplatesAsync(CancellationToken cancellationToken)
        {
            return await _client.GetAllTemplatesAsync();
        }
    }
}
