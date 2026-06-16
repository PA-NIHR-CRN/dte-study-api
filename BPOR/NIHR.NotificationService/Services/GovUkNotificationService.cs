using Microsoft.Extensions.Logging;
using NIHR.NotificationService.Enums;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using Notify.Client;
using Notify.Models.Responses;

namespace NIHR.NotificationService.Services
{
    internal class GovUkNotificationService : IDownstreamNotificationService
    {
        private readonly NotificationClient _client;
        private readonly ILogger<GovUkNotificationService> _logger;
        private readonly IReadOnlyDictionary<string, INotificationDeliveryHandler> _notificationStatusSinks;

        public GovUkNotificationService(NotificationClient client, ILogger<GovUkNotificationService> logger,
            IReadOnlyDictionary<string, INotificationDeliveryHandler> notificationStatusSinks)
        {
            _client = client;
            _logger = logger;
            _notificationStatusSinks = notificationStatusSinks;
        }

        public async Task<NotificationDeliveryStatus> SendNotification(SendNotificationRequest request, CancellationToken cancellationToken)
        {
            request.Validate();
            try
            {
                var personalisation = request.Personalisation.ToDictionary(x => x.Key, x => (dynamic)x.Value);

                switch (request.ContactMethod)
                {
                    case GovUkNotifyContactMethod.Email:
                        var emailAddress = request.Personalisation[PersonalisationKeys.Email];
                        await _client.SendEmailAsync(emailAddress, request.TemplateId, personalisation,
                            request.Reference.ToString());
                        return NotificationDeliveryStatus.Pending;
                    case GovUkNotifyContactMethod.Letter:
                        await _client.SendLetterAsync(request.TemplateId, personalisation,
                            request.Reference.ToString());
                        // Mark letters as delivered immediately.
                        return NotificationDeliveryStatus.Delivered;
                    default:
                        throw new NotSupportedException(
                            $"Contact method {request.ContactMethod} is not supported.");
                }
            }
            catch (HttpRequestException httpEx) when
                (httpEx.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                _logger.LogError(httpEx, "429 Rate Limit Exceeded error while sending notification");
                throw new InvalidOperationException("429 Rate Limit Exceeded error while sending notification.",
                    httpEx);
            }
        }

        public async Task<TemplateList> GetTemplates(CancellationToken cancellationToken)
        {
            return await _client.GetAllTemplatesAsync();
        }
    }
}