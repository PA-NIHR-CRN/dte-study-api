namespace BPOR.Rms.Services;

public interface IRandomiser
{
    public ICollection<T> GetRandomisedCollection<T>(IQueryable<T> collection, int count);
    
}
