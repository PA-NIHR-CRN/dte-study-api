using System.Threading;
using System.Threading.Tasks;

namespace NIHR.Infrastructure.Interfaces
{
    public interface IContentProvider
    {
        Task<string> GetContentAsync(string contentId, CancellationToken cancellationToken = default);
    }
}
