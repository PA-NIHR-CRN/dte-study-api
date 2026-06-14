using NIHR.NotificationService.Enums;
using NIHR.NotificationService.Models;

namespace NIHR.NotificationService.Interfaces;

public class NotificationServiceForHandler<THandler>(INotificationService notificationService) : INotificationService<THandler>
    where THandler : class, INotificationDeliveryHandler<THandler>
{
    public Task SendEmail(string reference, Dictionary<string, string> personalisation, string templateId, string emailAddress,
        CancellationToken cancellationToken)
    {
        return notificationService.SendNotificationAsync(
            new SendNotificationRequest()
            {
                ContactMethod = GovUkNotifyContactMethod.Email,
                EmailAddress = emailAddress,
                Reference = new NotificationReference(THandler.Key, reference),
                Personalisation = personalisation,
            }, cancellationToken);
    }
}