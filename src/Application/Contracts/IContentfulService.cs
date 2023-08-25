using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IContentfulService
    {
        Task<string> GetContentfulEntry(string entryId);
    }
}