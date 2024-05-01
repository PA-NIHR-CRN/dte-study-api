namespace BPOR.Rms.Startup
{
    public class CurrentUserProvider<T> : ICurrentUserProvider<T> where T : class?
    {
        private T? _user;

        public T? User { get => _user; set => _user = value; }
    }
}