using System.Diagnostics;
using Microsoft.Extensions.Logging;
using NIHR.NotificationService.Enums;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using Notify.Client;
using Notify.Models.Responses;

namespace NIHR.NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationClient _client;
        private readonly ILogger<NotificationService> _logger;
        private readonly IReadOnlyDictionary<string, INotificationStatusSink> _notificationStatusSinks;

        public NotificationService(NotificationClient client, ILogger<NotificationService> logger,
            IReadOnlyDictionary<string, INotificationStatusSink> notificationStatusSinks)
        {
            _client = client;
            _logger = logger;
            _notificationStatusSinks = notificationStatusSinks;
        }

        public async Task SendNotificationAsync(SendNotificationRequest request, CancellationToken cancellationToken)
        {
            request.Validate();

            var stopwatch = Stopwatch.StartNew();
            try
            {
                var personalisation = request.Personalisation.ToDictionary(x => x.Key, x => (dynamic)x.Value);
                
                switch (request.ContactMethod)
                {
                    case GovUkNotifyContactMethod.Email:
                        await _client.SendEmailAsync(request.EmailAddress, request.TemplateId, personalisation, request.Reference.ToString());
                        break;
                    case GovUkNotifyContactMethod.Letter:
                        await _client.SendLetterAsync(request.TemplateId, personalisation, request.Reference.ToString());
                        // Mark letters as delivered immediately.
                        await ProcessDeliveryCallback(request.Reference, NotificationDeliveryStatus.Delivered, cancellationToken); 
                        break;
                    default:
                        throw new NotSupportedException($"Contact method {request.ContactMethod} is not supported.");
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
                personalisation, request.Reference.ToString());
        }
        
        public async Task<TemplateList> GetTemplatesAsync(CancellationToken cancellationToken)
        {
            return await _client.GetAllTemplatesAsync();
        }

        public async Task ProcessDeliveryCallback(NotifyCallbackMessage message,
            CancellationToken cancellationToken)
        {
            NotificationReference notificationReference;

            if (int.TryParse(message.Reference, out _))
            {
                // This clause is to support the transition to the new notification reference scheme
                notificationReference = new NotificationReference("CMP", message.Reference);
            }
            else if (!NotificationReference.TryParse(message.Reference, out notificationReference!)) // This is the new reference format: "{upstream-key}:{upstream-ref}"
            {
                throw new Exception("Invalid reference");
            }
            
            var status = message.Status switch
            {
                "accepted" => NotificationDeliveryStatus.Accepted,
                "cancelled" => NotificationDeliveryStatus.Cancelled,
                "pending-virus-check" => NotificationDeliveryStatus.PendingVirusCheck,
                "virus-scan-failed" => NotificationDeliveryStatus.VirusScanFailed,
                "validation-failed" => NotificationDeliveryStatus.VirusScanFailed,
                "created" => NotificationDeliveryStatus.Created,
                "sending" => NotificationDeliveryStatus.Sending,
                "pending" => NotificationDeliveryStatus.Pending,
                "sent" => NotificationDeliveryStatus.Sent,
                "received" => NotificationDeliveryStatus.Received,
                "delivered" => NotificationDeliveryStatus.Delivered,
                "temporary-failure" => NotificationDeliveryStatus.TemporaryFailure,
                "permanent-failure" => NotificationDeliveryStatus.PermanentFailure,
                "technical-failure" => NotificationDeliveryStatus.TechnicalFailure,
                _ => throw new ArgumentOutOfRangeException(nameof(message.Status), message.Status, null)
            };

            await ProcessDeliveryCallback(notificationReference, status, cancellationToken);
        }

        private async Task ProcessDeliveryCallback(NotificationReference reference,
            NotificationDeliveryStatus status, CancellationToken cancellationToken)
        {

            
            if (!_notificationStatusSinks.TryGetValue(reference.UpstreamProviderKey, out var upstreamSink))
            {
                throw new Exception("Upstream sink not found");
            }

            await upstreamSink.HandleStatusChanged(reference.UpstreamReference, status, cancellationToken);
        }
    }
}
