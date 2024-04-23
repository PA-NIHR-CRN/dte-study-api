namespace NIHR.Infrastructure.Entities
{
    public interface ITimestamped
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
