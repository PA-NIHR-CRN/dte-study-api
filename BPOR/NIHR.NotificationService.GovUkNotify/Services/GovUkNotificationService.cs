using Microsoft.Extensions.Logging;
using NIHR.NotificationService.Enums;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using Notify.Client;
using Polly;
using Polly.Registry;

namespace NIHR.NotificationService.GovUkNotify.Services
{
    internal class GovUkNotificationService(
        NotificationClient client,
        ILogger<GovUkNotificationService> logger,
        ResiliencePipelineProvider<string> pipelineProvider)
        : IDownstreamNotificationService
    {
        public async Task<SendNotificationResult> SendNotification(SendNotificationRequest request, CancellationToken cancellationToken)
        {
            request.Validate();
            ResiliencePipeline pipeline = pipelineProvider.GetPipeline(DiExtensions.GovUkNotifyResiliencePipelineKey);
            return await pipeline.ExecuteAsync(pipelineCancellationToken =>
                SendNotificationInternal(request, pipelineCancellationToken), cancellationToken);
        }

        public async ValueTask<SendNotificationResult> SendNotificationInternal(SendNotificationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var personalisation = request.Personalisation.ToDictionary(x => x.Key, x => (dynamic)x.Value);

                switch (request.ContactMethod)
                {
                    case NotificationContactMethod.Email:
                        var emailAddress = request.Personalisation[PersonalisationKeys.Email];
                        await client.SendEmailAsync(emailAddress, request.TemplateId, personalisation,
                            request.Reference.ToString());
                        return SendNotificationResult.Success(NotificationDeliveryStatus.Pending);
                    case NotificationContactMethod.Letter:
                        await client.SendLetterAsync(request.TemplateId, personalisation,
                            request.Reference.ToString());
                        // Mark letters as delivered immediately.
                        return SendNotificationResult.Success(NotificationDeliveryStatus.Delivered);
                    default:
                        throw new NotSupportedException(
                            $"Contact method {request.ContactMethod} is not supported.");
                }
            }
            catch (HttpRequestException httpEx) when
                (httpEx.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                logger.LogWarning(httpEx, "429 Rate Limit Exceeded error while sending notification");
                return SendNotificationResult.TemporaryFailure("Rate limit exceeded");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Unable to send notification");
                return SendNotificationResult.PermanentFailure("Rate limit exceeded");
            }
        }
        
        public async Task<IEnumerable<Template>> GetTemplates(CancellationToken cancellationToken)
        {
            List<Template> result = new();
            foreach (var template in (await client.GetAllTemplatesAsync()).templates)
            {
                var contactMethod = ParseContactMethod(template.type);
                if (contactMethod.HasValue)
                {
                    result.Add(new Template
                    {
                        ContactMethod = contactMethod.Value,
                        Id = template.id,
                        Name = template.name,
                    });
                }
            }

            return result;
        }

        public NotificationContactMethod? ParseContactMethod(string templateType) =>
            templateType switch
            {
                "email" => NotificationContactMethod.Email,
                "letter" => NotificationContactMethod.Letter,
                _ => null
            };
    }
}