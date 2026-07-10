using System.Threading.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.AspNetCore.Authentication.ApiKey;
using NIHR.NotificationService.GovUkNotify.Controllers;
using NIHR.NotificationService.GovUkNotify.Services;
using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Settings;
using Notify.Client;
using Polly;
using Polly.Retry;

namespace NIHR.NotificationService.GovUkNotify;

public static class DiExtensions
{
    public const string GovUkNotifyResiliencePipelineKey = "govuk-notify-resilience-pipeline";
    
    public static void AddGovUkNotify(this IServiceCollection services)
    {        
        services.AddSingleton(s =>
             {
                 var options = s.GetRequiredService<IOptions<NotificationServiceSettings>>();
                 return new NotificationClient(options.Value.ApiKey);
             });
             
        // Rate limiting is purely reactive: Limit concurrent sends to 1 (may increase this depending on
        // performance); if a temporary failure is encountered (which includes HTTP 429) then delay
        // and retry up to 10 times before returning the temporary failure back to the caller (which is likely
        // to be a queue, so it can handle it as it sees fit).
        services.AddResiliencePipeline(GovUkNotifyResiliencePipelineKey, (builder, context) =>
        {
            builder
                .AddConcurrencyLimiter(
                    new ConcurrencyLimiterOptions()
                    {
                        PermitLimit = 1,
                        QueueLimit = 100 // allows for multiple sources, though in most scenarios there will only be one.
                    }
                )
                .AddRetry(
                    new RetryStrategyOptions()
                    {
                        ShouldHandle = new PredicateBuilder()
                            .HandleResult(i => i is SendNotificationResult
                            {
                                Status: SendNotificationStatus.TemporaryFailure
                            }),
                        Delay = TimeSpan.FromSeconds(5),
                        MaxDelay = TimeSpan.FromSeconds(60),
                        BackoffType = DelayBackoffType.Linear,
                        MaxRetryAttempts = 10,
                    });
        });

        services.AddOptions<NotificationServiceSettings>()
            .BindConfiguration("NotificationServiceSettings"); // TODO: Rename to GovUkNotify
        services.AddTransient<IDownstreamNotificationService, GovUkNotificationService>();
        services.AddApiKeyRoleFromOptions<NotificationServiceSettings>(i => i.BearerToken,
            NotifyCallbackController.RoleNameGovUkNotifyCallback);
    }
}