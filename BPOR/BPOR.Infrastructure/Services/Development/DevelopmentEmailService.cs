using BPOR.Domain.Settings;
using BPOR.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BPOR.Infrastructure.Services.Development;

public class DevelopmentEmailService(
    ILogger<DevelopmentEmailService> logger,
    IOptions<DevelopmentSettings> developmentSettings,
    IEmailService emailService)
    : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken)
    {
        if (developmentSettings.Value.ShouldBypassEmail)
        {
            logger.LogInformation(
                "Email sending is disabled. Email not sent to {Receiver} with subject {Subject} and body {Body}",
                to, subject, body);
        }
        else
        {
            await emailService.SendEmailAsync(to, subject, body, cancellationToken);
        }
    }
}
