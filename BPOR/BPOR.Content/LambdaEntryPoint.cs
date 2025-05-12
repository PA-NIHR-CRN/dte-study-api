using Amazon;
using Amazon.Lambda.ApplicationLoadBalancerEvents;
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Amazon.SecretsManager;
using NIHR.Infrastructure.Settings;

namespace BPOR.Content
{
    public class LambdaEntryPoint : ApplicationLoadBalancerFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                configurationBuilder.AddNihrConfiguration(context.HostingEnvironment);

                var config = configurationBuilder.Build();

                var secretsManagerSettings = config.GetSection(AwsSecretsManagerSettings.SectionName).Get<AwsSecretsManagerSettings>();

                if (secretsManagerSettings?.Enabled ?? true)
                {
                    var smConfig = new AmazonSecretsManagerConfig
                    {
                        Timeout = TimeSpan.FromSeconds(5),
                        RegionEndpoint = RegionEndpoint.GetBySystemName(secretsManagerSettings?.Region),
                    };

                    configurationBuilder.AddAwsSecretsManager(secretsManagerSettings?.SecretName,
                        () => new AmazonSecretsManagerClient(smConfig));
                }
            })
                .UseStartup<Startup>();
        }

        public override Task<ApplicationLoadBalancerResponse> FunctionHandlerAsync(ApplicationLoadBalancerRequest request, ILambdaContext lambdaContext)
        {
            return base.FunctionHandlerAsync(request, lambdaContext);
        }
    }
}
