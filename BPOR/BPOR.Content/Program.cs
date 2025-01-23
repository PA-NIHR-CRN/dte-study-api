using Amazon;
using Amazon.SecretsManager;
using NIHR.Infrastructure.Settings;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
              .ConfigureAppConfiguration((context, configurationBuilder) =>
                {
                    configurationBuilder.AddNihrConfiguration(context.HostingEnvironment);

                    var config = configurationBuilder.Build();

                    var secretsManagerSettings = config.GetSection(AwsSecretsManagerSettings.SectionName).Get<AwsSecretsManagerSettings>();

                    if (secretsManagerSettings?.Enabled ?? true)
                    {
                        configurationBuilder.AddAwsSecretsManager(secretsManagerSettings?.SecretName,
                            () => new AmazonSecretsManagerClient(
                                RegionEndpoint.GetBySystemName(secretsManagerSettings?.Region)));
                    }
                });

}
