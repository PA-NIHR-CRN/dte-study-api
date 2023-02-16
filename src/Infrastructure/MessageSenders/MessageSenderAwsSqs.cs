using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Application.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.MessageSenders
{
    public class MessageSenderAwsSqs : IMessageSender
    {
        private readonly string _queueUrl;
        private readonly IAmazonSQS _sqsClient;
        private readonly ILogger _logger;

        public MessageSenderAwsSqs(string queueUrl, IAmazonSQS sqsClient, ILogger logger)
        {
            _queueUrl = queueUrl;
            _sqsClient = sqsClient;
            _logger = logger;
        }

        public async Task SendAsync(IEnumerable<object> requests)
        {
            var requestEntries =
                requests
                    .Select((x, index) => new SendMessageBatchRequestEntry(index.ToString(), JsonConvert.SerializeObject(x))
                    {
                        MessageGroupId = GetType().Assembly.GetName().Name
                    })
                    .ToList();

            var request = new SendMessageBatchRequest
            {
                QueueUrl = _queueUrl,
                Entries = requestEntries
            };

            _logger.LogInformation($"SQS: sending multiple batched : {requestEntries.Count} messages");
            var response = await _sqsClient.SendMessageBatchAsync(request);
            _logger.LogInformation($"SQS: {response.Successful.Count} messages sent");

            foreach (var failed in response.Failed)
            {
                _logger.LogError($"SQS: failed messageId: {failed.Id}, Code: {failed.Code}, Message: {failed.Message}, SenderFault: {failed.SenderFault}");
            }
        }

        public async Task SendAsync(object request)
        {
            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = JsonConvert.SerializeObject(request),
                MessageGroupId = GetType().Assembly.GetName().Name
            };

            await _sqsClient.SendMessageAsync(sendMessageRequest);
            
            _logger.LogInformation($"SQS: Message Sent on queueUrl: ");
        }
    }
}