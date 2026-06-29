namespace NIHR.NotificationService.Interfaces;

public interface INotificationDeliveryHandler<T> : INotificationDeliveryHandler
    where T : INotificationDeliveryHandler<T>
{
    static abstract string Key { get; }
}