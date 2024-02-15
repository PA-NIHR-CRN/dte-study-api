using System;
using System.Threading.Tasks;
using Application.Contracts;

namespace Infrastructure.Services.Development
{
    public class NullEmailService : IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
            // log the email instead
            Console.WriteLine($"Email sent to {to} with subject {subject} and body {body}");
            return Task.CompletedTask;
        }
    }
}
