namespace Application.Settings
{
    public class CpmsSettings
    {
        public static string SectionName => "Cpms";
        public string CpmsApiBaseUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}