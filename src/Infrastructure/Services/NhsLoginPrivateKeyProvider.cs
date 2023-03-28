using Application.Contracts;
using Application.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class NhsLoginPrivateKeyProvider : IPrivateKeyProvider
    {
        private readonly NhsLoginSettings nhsLoginSettings;

        public NhsLoginPrivateKeyProvider(IOptions<NhsLoginSettings> nhsLoginSettings)
        {
            this.nhsLoginSettings = nhsLoginSettings.Value;
        }

        public async Task<RSA> GetPrivateKeyAsync(CancellationToken cancellationToken)
        {
            var keyText = nhsLoginSettings.PrivateKey;
            if (string.IsNullOrWhiteSpace(keyText))
            {
                keyText = await File.ReadAllTextAsync(nhsLoginSettings.PemFilePath, cancellationToken);
            }

            var rsa = RSA.Create();
            rsa.ImportFromPem(keyText);

            return rsa;
        }
    }
}
