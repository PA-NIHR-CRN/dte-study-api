using System;

namespace StudyApi.Requests.Users
{
    public class VerifyMfaRequest
    {
        public string AuthenticatorAppCode { get; set; }
        public string MfaDetails { get; set; }
        public string SessionId { get; set; }
    }
}