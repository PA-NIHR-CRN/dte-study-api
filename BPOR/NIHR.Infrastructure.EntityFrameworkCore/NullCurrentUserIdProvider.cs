namespace NIHR.Infrastructure.EntityFrameworkCore
{
    internal class NullCurrentUserIdProvider : ICurrentUserIdProvider
    {
        public int? UserId => null;
    }
}