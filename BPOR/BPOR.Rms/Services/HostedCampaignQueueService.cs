namespace BPOR.Rms.Services;

public class HostedCampaignQueueService(
    IRmsTaskQueue taskQueue,
    ILogger<HostedCampaignQueueService> logger,
    IServiceProvider serviceProvider)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("HostedCampaignQueueService is running");

        await BackgroundProcessing(stoppingToken);
    }

    private async Task BackgroundProcessing(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var id = await taskQueue.DequeueAsync(stoppingToken);

            try
            {
                using var scope = serviceProvider.CreateScope();
                var campaignService = scope.ServiceProvider.GetRequiredService<ICampaignService>();
                await campaignService.SendCampaignAsync(id, stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred executing {WorkItem}", nameof(id));
            }
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        await base.StopAsync(stoppingToken);
    }
}
