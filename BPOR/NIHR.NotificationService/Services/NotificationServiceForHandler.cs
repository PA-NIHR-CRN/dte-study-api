using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;

namespace NIHR.NotificationService.Services;

internal class NotificationServiceForHandler<THandler>(INotificationService notificationService) : INotificationService<THandler>
    where THandler : class, INotificationDeliveryHandler<THandler>
{
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
            Personalisation = value.Personalisation,
            Reference = new NotificationReference(THandler.Key, value.Reference)
        };
    }
}