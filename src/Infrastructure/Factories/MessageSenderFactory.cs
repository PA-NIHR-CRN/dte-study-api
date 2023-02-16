using System;
using Amazon.SQS;
using Application.Contracts;
using Application.Settings;
using Infrastructure.MessageSenders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Factories
{
    public class MessageSenderFactory : IMessageSenderFactory
    {
        private readonly AwsSettings _awsSettings;
        private readonly ILogger<MessageSenderFactory> _logger;
        private readonly IServiceProvider _serviceProvider;

        public MessageSenderFactory(AwsSettings awsSettings, ILogger<MessageSenderFactory> logger, IServiceProvider serviceProvider)
        {
            _awsSettings = awsSettings;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public IMessageSender Build(string source)
        {
            if (string.IsNullOrEmpty(_awsSettings.SomeQueueUrl))
            {
                _logger.LogWarning($"Configuring '{nameof(MessageSenderInMemory)}' only for testing!");

                return new MessageSenderInMemory();
            }
            
            _logger.LogInformation($"Configuring '{nameof(MessageSenderAwsSqs)}'");
            var sqsClient = _serviceProvider.GetService<IAmazonSQS>();
            
            return new MessageSenderAwsSqs(_awsSettings.SomeQueueUrl, sqsClient, _logger);
        }
    }
}