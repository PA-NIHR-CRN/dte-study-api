using Microsoft.EntityFrameworkCore;

namespace NIHR.NotificationService.Entities;

public class NotificationDbContext : DbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Notification> Notifications { get; set; }
    public DbSet<NotificationData> NotificationDatas { get; set; }
}
