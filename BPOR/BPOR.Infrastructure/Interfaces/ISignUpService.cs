using BPOR.Infrastructure.Responses.V1.Users;
using Dte.Common.Responses;

namespace BPOR.Infrastructure.Interfaces;

public interface ISignUpService
{
    Task<Response<SignUpResponse>> SignUpAsync(string email, string password, string selectedLocale,
        CancellationToken cancellationToken);
}
