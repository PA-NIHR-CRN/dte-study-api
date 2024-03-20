using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using BPOR.Domain.Settings;
using BPOR.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;

namespace BPOR.Infrastructure.Services;

public class EmailService(IOptions<EmailSettings> emailSettings, IAmazonSimpleEmailService client) : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken)
    {
        var from = emailSettings.Value.FromAddress;
        var request = new SendEmailRequest
        {
            Source = from,
            Destination = new Destination
            {
                ToAddresses = [to]
            },
            Message = new Message
            {
                Subject = new Content(subject),
                Body = new Body
                {
                    Html = new Content(body)
                }
            }
        };

        await client.SendEmailAsync(request, cancellationToken);
    }
}
