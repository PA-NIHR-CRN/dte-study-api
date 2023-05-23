namespace Application.Settings
{
    public class DevSettings
    {
        public static string SectionName => "DevSettings";
        public bool AutoConfirmNewCognitoSignup { get; set; }
        public bool EnableStubs { get; set; }
    }
}