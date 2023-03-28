using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Settings
{
    public class NhsLoginSettings
    {
        public string ClientId { get; set; }
        public string BaseUrl { get; set; }
        public string TokenEndpoint { get; set; }
        public string UserInfoEndpoint { get; set; }

        // Private key must be supplied using either PemFilePath or PrivateKey.
        // If both are specified, PrivateKey takes precedence.
        // PemFilePath specifies the file path to the PEM file holding the private key.
        public string PemFilePath { get; set; } 

        // PrivateKey holds the key contents directly.
        public string PrivateKey { get; set; }
    }
}
