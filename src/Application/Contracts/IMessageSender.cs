using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IMessageSender
    {
        Task SendAsync(IEnumerable<object> requests);
        Task SendAsync(object request);
    }
}