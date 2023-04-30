namespace Application.Settings
{
    public class DevSwitches
    {
        public static string SectionName => "DevSwitches";
        public bool AutoConfirmNewCognitoSignup { get; set; }
        public bool EnableStubs { get; set; }
    }
}