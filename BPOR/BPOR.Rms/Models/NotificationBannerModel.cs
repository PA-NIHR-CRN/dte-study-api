using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BPOR.Rms.Models;

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
    public static void AddSuccessNotification(this ViewDataDictionary viewData, string body)
    {
        viewData.AddNotification(new NotificationBannerModel
        {
            IsSuccess = true,
            Heading = "Success",
            Body = body
        });
    }

    public static void AddNotification(this ViewDataDictionary viewData, NotificationBannerModel notification)
    {
        viewData["Notification"] = notification;
    }
}
