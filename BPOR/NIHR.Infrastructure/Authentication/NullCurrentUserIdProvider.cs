namespace NIHR.Infrastructure.EntityFrameworkCore
{
    public class NullCurrentUserIdProvider<T> : ICurrentUserIdAccessor<T> where T : struct
    { 
        public T? UserId => default;

        public void SetCurrentUserId(T id)
        {
        }
    }
}