using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using BPOR.Domain.Settings;
using BPOR.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;

namespace BPOR.Infrastructure.Services;

public class EmailService(IOptions<EmailSettings> emailSettings) : IEmailService
{
    private readonly IAmazonSimpleEmailService _client = new AmazonSimpleEmailServiceClient(RegionEndpoint.EUWest2);

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

        await _client.SendEmailAsync(request, cancellationToken);
    }
}
