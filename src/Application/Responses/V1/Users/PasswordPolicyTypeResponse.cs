namespace Application.Responses.V1.Users
{
    public class PasswordPolicyTypeResponse
    {
        public int MinimumLength { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireNumbers { get; set; }
        public bool RequireSymbols { get; set; }
        public bool RequireUppercase { get; set; }
        public string AllowedPasswordSymbols { get; set; }
        public string[] WeakPasswords { get; set; }
    }
}