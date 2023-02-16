using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contracts;

namespace Infrastructure.MessageSenders
{
    public class MessageSenderInMemory : IMessageSender
    {
        public ConcurrentQueue<dynamic> Queue { get; } = new ConcurrentQueue<dynamic>();

        public Task SendAsync(IEnumerable<object> requests)
        {
            foreach (var cloudEventRequest in requests)
            {
                Queue.Enqueue(cloudEventRequest);
            }

            return Task.CompletedTask;
        }

        public Task SendAsync(object request)
        {
            Queue.Enqueue(request);
            
            return Task.CompletedTask;
        }
    }
}