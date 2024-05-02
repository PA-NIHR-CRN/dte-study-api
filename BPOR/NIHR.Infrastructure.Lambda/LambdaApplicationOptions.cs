namespace NIHR.Infrastructure.Lambda
{
    public class LambdaApplicationOptions
    {
        public string[]? Args { get; set; }
        public string? ApplicationName { get; set; }
        public string? EnvironmentName { get; set; }
        public string? ContentRootPath { get; set; }
    }
}