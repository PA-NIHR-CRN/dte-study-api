using Amazon.SQS;
using Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StudyApi.DependencyRegistrations
{
    public static class MessagingRegistration
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            // AWS
            var awsSettings = configuration.GetSection(AwsSettings.SectionName).Get<AwsSettings>();
            var amazonSqsConfig = new AmazonSQSConfig();

            if (!string.IsNullOrWhiteSpace(awsSettings.ServiceUrl))
            {
                amazonSqsConfig.ServiceURL = awsSettings.ServiceUrl;
            }
            
            services.AddTransient<IAmazonSQS>(_ => new AmazonSQSClient(amazonSqsConfig));
            
            return services;
        }
    }
}