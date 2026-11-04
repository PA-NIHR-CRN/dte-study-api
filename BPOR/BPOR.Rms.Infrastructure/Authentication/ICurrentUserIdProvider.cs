namespace BPOR.Rms.Infrastructure.Authentication
{
    public interface ICurrentUserIdProvider<T> where T : struct
    {
        T? UserId { get; }
        bool SuppressUnknownUserIdWarning { get; }
    }
}