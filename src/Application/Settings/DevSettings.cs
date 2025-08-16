namespace Application.Settings
{
    public class DevSettings
    {
        public static string SectionName => "DevSettings";
        public bool AutoConfirmNewCognitoSignup { get; set; }
        public bool EnableStubs { get; set; }
        public bool ShouldBypassEmail { get; set; }
        public bool IsInMaintenance { get; set; }
        public bool BypassMfa { get; set; }
    }
}
