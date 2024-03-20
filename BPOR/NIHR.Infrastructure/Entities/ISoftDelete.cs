namespace NIHR.Infrastructure.Entities
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
