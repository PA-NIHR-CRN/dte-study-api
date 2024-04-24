namespace NIHR.Infrastructure.EntityFrameworkCore
{
    public interface ICurrentUserIdProvider
    {
        int? UserId { get; }
    }
}