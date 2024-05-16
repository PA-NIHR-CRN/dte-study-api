using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using Notify.Client;
using Notify.Models.Responses;
using Polly;

namespace NIHR.NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationClient _client;

        public NotificationService(NotificationClient client)
        {
            _client = client;
        }

        public async Task SendEmailAsync(SendEmailRequest request, CancellationToken cancellationToken)
        {
            await _client.SendEmailAsync(request.EmailAddress, request.EmailTemplateId.ToString(), request.Personalisation, request.Reference);
        }

        public async Task<EmailNotificationResponse> SendBatchEmailAsync(SendBatchEmailRequest request,
            CancellationToken cancellationToken)
        {
            const int batchSize = 100;
            const int rateLimit = 3000; 

            var rateLimitPolicy = Policy.RateLimitAsync(rateLimit, TimeSpan.FromMinutes(1));

            var tasks = new List<Task>();

            var batches = request.EmailAddresses
                .Select((email, index) => new { email, index })
                .GroupBy(x => x.index / batchSize)
                .Select(g => g.Select(x => x.email).ToList())
                .ToList();

            foreach (var batch in batches)
            {
                var batchTasks = batch.Select(async email =>
                {
                    if (!request.PersonalisationData.TryGetValue(email, out var personalisation))
                    {
                        throw new KeyNotFoundException($"Personalisation data not found for email: {email}");
                    }

                    if (!personalisation.TryGetValue("emailCampaignParticipantId", out var reference))
                    {
                        throw new KeyNotFoundException($"EmailCampaignParticipantId not found for email: {email}");
                    }

                    await rateLimitPolicy.ExecuteAsync(() =>
                        _client.SendEmailAsync(email, request.EmailTemplateId.ToString(), personalisation, reference.ToString()));
                });

                tasks.Add(Task.WhenAll(batchTasks));
            }

            await Task.WhenAll(tasks);

            return new EmailNotificationResponse();
        }

        public async Task<TemplateList> GetTemplatesAsync(CancellationToken cancellationToken)
        {
            return await _client.GetAllTemplatesAsync();
        }
    }
}
