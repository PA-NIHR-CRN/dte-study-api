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

        public string AuthorizeEndpoint { get; set; }
        public string Scope { get; set; }
        public string RedirectUri { get; set; }
    }
}
