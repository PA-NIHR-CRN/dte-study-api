using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

[ApiController]
[Route("api/auth/nhs")]
public class NhsAuthController : ControllerBase
{
    private readonly INhsLoginStateStore _stateStore;
    private readonly NhsLoginSettings _settings;

    public NhsAuthController(INhsLoginStateStore stateStore, IOptions<NhsLoginSettings> settings)
    {
        _stateStore = stateStore;
        _settings = settings.Value;
    }

    [AllowAnonymous]
    [HttpGet("start")]
    public async Task<IActionResult> Start(CancellationToken ct)
    {
        var state = NewToken();
        var nonce = NewToken();

        await _stateStore.StoreAsync(state, nonce, ct);

        var url =
            $"{_settings.AuthorizeEndpoint}"
            + $"?client_id={Uri.EscapeDataString(_settings.ClientId)}"
            + $"&scope={Uri.EscapeDataString(_settings.Scope)}"
            + $"&response_type=code"
            + $"&redirect_uri={Uri.EscapeDataString(_settings.RedirectUri)}"
            + $"&state={Uri.EscapeDataString(state)}"
            + $"&nonce={Uri.EscapeDataString(nonce)}";

        return Redirect(url);
    }

    private static string NewToken(int bytes = 32)
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(bytes)).ToLowerInvariant();
    }
}
