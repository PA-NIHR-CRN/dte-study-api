using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BPOR.Rms.Infrastructure.Models;

namespace BPOR.Rms.Infrastructure.Interfaces
{
    public interface IPostcodeMapper
    {
        Task<IEnumerable<PostcodeAddressModel>> GetAddressesByPostcodeAsync(string postcode,
            CancellationToken cancellationToken);

        Task<CoordinatesModel> GetCoordinatesFromPostcodeAsync(string postcode,
            CancellationToken cancellationToken);
    }
}
