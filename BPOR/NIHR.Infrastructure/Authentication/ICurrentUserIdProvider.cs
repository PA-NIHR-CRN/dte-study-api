namespace NIHR.Infrastructure
{
    public interface ICurrentUserIdProvider
    {
        int? UserId { get; }
    }
}