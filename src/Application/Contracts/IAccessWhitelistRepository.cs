using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Users;

namespace Application.Contracts
{
    public interface IAccessWhitelistRepository
    {
        Task<AccessWhitelist> GetWhitelistByEmail(string email);
        Task<IEnumerable<AccessWhitelist>> GetWhitelist();
        Task SaveWhitelist(IEnumerable<string> emails);
    }
}