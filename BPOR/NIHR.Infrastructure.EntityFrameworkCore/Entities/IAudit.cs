namespace NIHR.Infrastructure.Entities;

public interface IAudit
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    int CreatedById { get; set; }
    int UpdatedById { get; set; }
}
