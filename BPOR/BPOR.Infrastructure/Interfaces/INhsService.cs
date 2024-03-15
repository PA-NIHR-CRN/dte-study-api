
using BPOR.Infrastructure.Responses.V1.Nhs;
using BPOR.Infrastructure.Responses.V1.Users;
using Dte.Common.Responses;

namespace BPOR.Infrastructure.Interfaces;

public interface INhsService
{
    Task<Response<NhsLoginResponse>> NhsLoginAsync(string code, string redirectUrl, string selectedLocale,
        CancellationToken cancellationToken);
    Task<Response<SignUpResponse>> NhsSignUpAsync(bool consent, string selectedLocale, string token,
        CancellationToken cancellationToken);
}
