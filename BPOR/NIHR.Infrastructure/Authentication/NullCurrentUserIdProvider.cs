namespace NIHR.Infrastructure.EntityFrameworkCore
{
    public class NullCurrentUserIdProvider : ICurrentUserIdProvider
    {
        public int? UserId => null;
    }
}