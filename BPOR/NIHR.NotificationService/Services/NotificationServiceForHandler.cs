using NIHR.NotificationService.Enums;
using NIHR.NotificationService.Models;

namespace NIHR.NotificationService.Interfaces;

public class NotificationServiceForHandler<THandler>(INotificationService notificationService) : INotificationService<THandler>
    where THandler : class, INotificationDeliveryHandler<THandler>
{
    public Task SendEmail(string reference, Dictionary<string, string> personalisation, string templateId, string emailAddress,
        CancellationToken cancellationToken)
    {
        return notificationService.SendNotification(
            new SendNotificationRequest()
            {
                ContactMethod = GovUkNotifyContactMethod.Email,
                EmailAddress = emailAddress,
                TemplateId = templateId,
                Reference = new NotificationReference(THandler.Key, reference),
                Personalisation = personalisation,
            }, cancellationToken);
    }

    public Task SendNotifications(IEnumerable<UnkeyedSendNotificationRequest> notifications, CancellationToken cancellationToken)
    {
        return notificationService.SendNotifications(notifications.Select(Qualify), cancellationToken);
    }

    private SendNotificationRequest Qualify(UnkeyedSendNotificationRequest value)
    {
        return new SendNotificationRequest
        {
            ContactMethod = value.ContactMethod,
            TemplateId = value.TemplateId,
            EmailAddress = value.EmailAddress,
            Personalisation = value.Personalisation,
            Reference = new NotificationReference(THandler.Key, value.Reference)
        };
    }
}