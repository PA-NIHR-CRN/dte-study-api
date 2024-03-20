using System.Security.Cryptography;
using BPOR.Domain.Settings;
using BPOR.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;

namespace BPOR.Infrastructure.Services;

public class NhsLoginPrivateKeyProvider(IOptions<NhsLoginSettings> nhsLoginSettings) : IPrivateKeyProvider
{
    public async Task<RSA> GetPrivateKeyAsync(CancellationToken cancellationToken)
    {
        var keyText = nhsLoginSettings.Value.PrivateKey;
        if (string.IsNullOrWhiteSpace(keyText))
        {
            keyText = await File.ReadAllTextAsync(nhsLoginSettings.Value.PemFilePath, cancellationToken);
        }

        var rsa = RSA.Create();
        rsa.ImportFromPem(keyText);

        return rsa;
    }
}
