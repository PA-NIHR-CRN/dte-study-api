namespace BPOR.Rms.Infrastructure.Authentication
{
    public interface ICurrentUserIdAccessor<T> : ICurrentUserIdProvider<T> where T : struct
    {
        void SetCurrentUserId(T id);
    }
}