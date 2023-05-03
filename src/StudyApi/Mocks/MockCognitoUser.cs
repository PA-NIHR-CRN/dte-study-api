namespace StudyApi.Mocks;

public class MockCognitoUser
{
    public MockCognitoUser(string username, string password, string email, bool isConfirmed, string userStatus)
    {
        Username = username;
        Password = password;
        Email = email;
        IsConfirmed = isConfirmed;
        UserStatus = userStatus;
    }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool IsConfirmed { get; set; }
    public string UserStatus { get; set; }
}