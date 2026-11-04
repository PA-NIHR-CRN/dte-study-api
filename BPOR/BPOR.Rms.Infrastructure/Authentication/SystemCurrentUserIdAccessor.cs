namespace BPOR.Rms.Infrastructure.Authentication
{
    public class SystemCurrentUserIdAccessor<T> : ICurrentUserIdAccessor<T> where T : struct
    {

        protected T? _userId;

        public T? UserId => _userId;

        public bool SuppressUnknownUserIdWarning => true;

        public void SetCurrentUserId(T id)
        {
            _userId = id;
        }
    }
}
