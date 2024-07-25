using BPOR.Rms.Controllers;
using HandlebarsDotNet;
using NIHR.Infrastructure.Interfaces;

namespace BPOR.Rms.Services
{
    public class TransactionalEmailService(IEmailService emailService, IContentProvider contentProvider) : ITransactionalEmailService
    {
        public async Task<EmailTemplate> SendAsync<TData>(string emailAddress, string templateName, TData data, CancellationToken token = default)
        {
            var emailContent = await GetEmailContent(templateName, data, token);
            await emailService.SendEmailAsync(emailAddress, emailContent.EmailSubject, emailContent.EmailBody, token);

            return emailContent;
        }

        protected async Task<EmailTemplate> GetEmailContent<TData>(string templateName, TData data, CancellationToken token)
        {
            var source = await contentProvider.GetContentAsync<EmailTemplate>(templateName, token);

            if (!string.IsNullOrWhiteSpace(source.EmailBody))
            {
                var template = Handlebars.Compile(source.EmailBody);

                source.EmailBody = Dte.Common.Content.CustomMessageEmail.GetCustomMessageHtml().Replace("###BODY_REPLACE###", template(data));
            }

            if (!string.IsNullOrWhiteSpace(source.EmailSubject))
            {
                var template = Handlebars.Compile(source.EmailSubject);
                source.EmailSubject = template(data);
            }

            return source;
        }
    }
}
