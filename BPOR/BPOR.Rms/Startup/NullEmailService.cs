using NIHR.Infrastructure.Interfaces;

namespace BPOR.Rms.Startup
{
    public class NullEmailService : IEmailService
    {
        private readonly ILogger<NullEmailService> _logger;

        public NullEmailService(ILogger<NullEmailService> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("to: {to}, subject: {subject}, body: {body}", to, subject, body);

            return Task.CompletedTask;
        }
    }
}