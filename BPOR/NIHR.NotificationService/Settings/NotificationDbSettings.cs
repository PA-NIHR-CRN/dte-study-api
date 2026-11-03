using NIHR.Infrastructure.EntityFrameworkCore;

namespace NIHR.NotificationService.Settings;

public class NotificationDbSettings : DbSettings
{
    public static string SectionName => "NotificationDbSettings";
}