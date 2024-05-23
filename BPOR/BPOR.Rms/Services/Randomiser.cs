using Microsoft.EntityFrameworkCore;

namespace BPOR.Rms.Services;

public class Randomiser() : IRandomiser
{
    public ICollection<T> GetRandomisedCollection<T>(IQueryable<T> queryable, int count)
    {
        return queryable.OrderBy(x => EF.Functions.Random()).Take(count).ToList();
    }
}
