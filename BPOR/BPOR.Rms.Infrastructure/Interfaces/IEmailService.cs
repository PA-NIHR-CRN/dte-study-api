using System.Threading;
using System.Threading.Tasks;

namespace BPOR.Rms.Infrastructure.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
    }
}
