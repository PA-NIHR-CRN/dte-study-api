using Microsoft.AspNetCore.Mvc;
using NIHR.Infrastructure.AspNetCore;

namespace NIHR.GovUk.AspNetCore.Mvc;

public class NotificationBannerModel
{
    public bool IsSuccess { get; set; }
    public string Heading { get; set; }
    public string Body { get; set; }
    public string? SubBodyText { get; set; }
    public string? LinkText { get; set; }
    public string? LinkUrl { get; set; }
}

public static class NotificationBannerExtensions
{
    public static void AddSuccessNotification(this Controller controller, string body)
    {
        controller.AddNotification(new NotificationBannerModel
        {
            IsSuccess = true,
            Heading = "Success",
            Body = body
        });
    }

    public static void AddNotification(this Controller controller, NotificationBannerModel notification)
    {
        controller.TempData.Put("Notification", notification);
    }
}