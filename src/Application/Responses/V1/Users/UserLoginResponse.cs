namespace Application.Responses.V1.Users
{
    public class UserLoginResponse
    {
        public string IdToken { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; }
        public string ErrorMessage { get; set; }
    }
}