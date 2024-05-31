﻿using NIHR.NotificationService.Interfaces;
using NIHR.NotificationService.Models;
using Notify.Models.Responses;

namespace BPOR.Rms.Startup
{
    public class NullNotificationService : INotificationService
    {
        public Task SendEmailAsync(SendEmailRequest request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task<EmailNotificationResponse> SendBatchEmailAsync(SendBatchEmailRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new EmailNotificationResponse());
        }

        public Task<TemplateList> GetTemplatesAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new TemplateList());
        }
    }
}
