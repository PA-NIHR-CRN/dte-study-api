namespace BPOR.Rms.Services;

public class Randomiser(Random random) : IRandomiser
{
    public ICollection<T> GetRandomisedCollection<T>(IQueryable<T> queryable, int count)
    {
        int totalItems = queryable.Count();

        if (count > totalItems)
        {
            throw new ArgumentException("Requested count exceeds the total number of items available.");
        }

        var randomIndices = Enumerable.Range(0, totalItems)
            .OrderBy(_ => random.Next())
            .Take(count)
            .ToList();

        return queryable.Where((item, index) => randomIndices.Contains(index)).ToList();
        
    }
}
