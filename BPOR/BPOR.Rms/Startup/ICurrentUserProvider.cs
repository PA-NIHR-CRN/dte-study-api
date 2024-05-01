namespace BPOR.Rms.Startup
{
    public interface ICurrentUserProvider<T> where T : class?
    {
        public T? User { get; set; }
    }
}