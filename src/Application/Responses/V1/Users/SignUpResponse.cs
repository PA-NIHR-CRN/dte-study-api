namespace Application.Responses.V1.Users
{
    public class SignUpResponse
    {
        
        public string UserId { get; set; }
        public bool IsSuccess { get; set; }
        public bool UserConsents { get; set; }
    }
}