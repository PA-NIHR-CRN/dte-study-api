using System.Threading.Tasks;
using Application.Responses.V1;

namespace Application.Contracts
{
    public interface IContentfulService
    {
        Task<ContentfulEmail> GetContentfulEmailAsync(string entryId, string locale);
    }
}