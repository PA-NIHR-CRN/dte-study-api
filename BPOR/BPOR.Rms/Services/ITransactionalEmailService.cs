using BPOR.Rms.Controllers;

namespace BPOR.Rms.Services
{
    public interface ITransactionalEmailService
    {
        Task<EmailTemplate> SendAsync<TData>(string emailAddress, string templateName, TData data, CancellationToken token = default);
    }
}