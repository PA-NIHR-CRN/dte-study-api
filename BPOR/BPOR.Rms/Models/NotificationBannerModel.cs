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
