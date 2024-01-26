using System;
using System.Threading.Tasks;
using Application.Contracts;

namespace Infrastructure.Services.Mocks
{
    public class MockEmailService : IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
            // log the email instead
            Console.WriteLine($"Email sent to {to} with subject {subject} and body {body}");
            return Task.CompletedTask;
        }
    }
}
