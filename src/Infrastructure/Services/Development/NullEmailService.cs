﻿using System.Threading.Tasks;
using Application.Contracts;
using Application.Settings;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Development;

public class NullEmailService : IEmailService
{
    private readonly ILogger<NullEmailService> _logger;
    private readonly IEmailService _emailService;
    private readonly DevSettings _devSettings;

    public NullEmailService(ILogger<NullEmailService> logger, IEmailService emailService, DevSettings devSettings)
    {
        _logger = logger;
        _emailService = emailService;
        _devSettings = devSettings;
    }

    public Task SendEmailAsync(string to, string subject, string body)
    {
        if (_devSettings.ShouldBypassEmail)
        {
            _logger.LogInformation(
                "Email sending is disabled. Email not sent to {Receiver} with subject {Subject} and body {Body}", to,
                subject, body);
        }
        else
        {
            _emailService.SendEmailAsync(to, subject, body);
        }

        return Task.CompletedTask;
    }
}
