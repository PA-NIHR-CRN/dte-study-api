using NIHR.Infrastructure.Models;
using NIHR.NotificationService.Interfaces;
using Notify.Client;
using Notify.Models.Responses;

namespace NIHR.NotificationService.Services;

public class NotificationService : INotificationService
{
    private readonly NotificationClient _client;

    public NotificationService(NotificationClient client)
    {
        _client = client;
    }
    
    public async Task SendEmailAsync(SendEmailRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<EmailNotificationResponse> SendBatchEmailAsync(SendBatchEmailRequest request, CancellationToken cancellationToken)
    {
        //    string emailAddress, 
        // string templateId, 
        // Dictionary<string,dynamic> personalisation = null, 
        // string clientReference = null, 
        // string emailReplyToId = null)
        foreach (var email in request.EmailAddresses)
        {
            await _client.SendEmailAsync("paddy.duff31@gmail.com", request.EmailTemplateId.ToString());
        }
        return new EmailNotificationResponse();
    }

    public async Task<TemplateList> GetTemplatesAsync(CancellationToken cancellationToken)
    {
        return await _client.GetAllTemplatesAsync();
    }
}
