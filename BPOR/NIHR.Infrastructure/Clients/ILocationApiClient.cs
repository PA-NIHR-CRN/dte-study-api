using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NIHR.Infrastructure.Models;

namespace NIHR.Infrastructure.Clients
{
    public interface ILocationApiClient
    {
        Task<IEnumerable<PostcodeAddressModel>> GetAddressesByPostcodeAsync(string postcode,
            CancellationToken cancellationToken);
    }
}
