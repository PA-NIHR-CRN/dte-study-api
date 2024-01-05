using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Responses.V1.Addresses;
using Dte.Common.Responses;

namespace Application.Contracts;

public interface ILocationService
{
    Task<Response<IEnumerable<PostcodeAddressModel>>> GetAddressesByPostcodeAsync(string postcode);
}
