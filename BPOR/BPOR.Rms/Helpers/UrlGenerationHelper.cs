using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BPOR.Rms.Helpers;

public class UrlGenerationHelper(
    IUrlHelperFactory urlHelperFactory,
    IActionContextAccessor actionContextAccessor,
    IHttpContextAccessor httpContextAccessor)
{
    private readonly IUrlHelper _urlHelper =
        urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext ?? throw new InvalidOperationException());

    public string GenerateRegisterInterestUrl(string encryptedReference)
    {
        var path = _urlHelper.Action("RegisterInterest", "NotifyCallback", new { reference = encryptedReference });
        return $"{GetBaseUrl()}{path}";
    }

    private string GetBaseUrl()
    {
        var request = httpContextAccessor.HttpContext.Request;
        return $"{request.Scheme}://{request.Host}";
    }
}
