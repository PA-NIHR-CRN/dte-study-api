namespace BPOR.Rms.Services;

public class Randomiser(Random random) : IRandomiser
{
    public ICollection<T> GetRandomisedCollection<T>(IQueryable<T> queryable, int count)
    {
        // TODO can be done not in memory?
        var allItems = queryable.ToList(); 
        
        int totalItems = allItems.Count;
        if (count > totalItems)
        {
            throw new ArgumentException("Requested count exceeds the total number of items available.");
        }

        var randomIndices = Enumerable.Range(0, totalItems)
            .OrderBy(_ => random.Next())
            .Take(count)
            .ToList();

        return allItems.Where((item, index) => randomIndices.Contains(index)).ToList();
        
    }
}
