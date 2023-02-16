using System.Threading.Tasks;
using Application.Responses.V1.Cpms;

namespace Application.Contracts
{
    public interface ICpmsHttpClient
    {
        Task<CpmsApiResponseRoot> GetStudyAsync(long id);
    }
}