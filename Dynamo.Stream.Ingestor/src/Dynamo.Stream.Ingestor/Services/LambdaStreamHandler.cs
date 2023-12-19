using Amazon.Lambda;
using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Amazon.Lambda.Model;
using Dynamo.Stream.Handler.Handlers;
using Microsoft.Extensions.Options;

namespace Dynamo.Stream.Ingestor.Services
{
    public class LambdaStreamHandler : ILambdaStreamHandler
    {
        private readonly ILambdaSerializer _serializer;
        private readonly IAmazonLambda _lambdaClient;
        private readonly StreamHandlerLambdaSettings _streamHandlerLambdaSettings;

        public LambdaStreamHandler(ILambdaSerializer serializer, IAmazonLambda lambdaClient, IOptions<StreamHandlerLambdaSettings> streamHandlerLambdaSettings)
        {
            _serializer = serializer;
            _lambdaClient = lambdaClient;
            _streamHandlerLambdaSettings = streamHandlerLambdaSettings.Value;
        }

        public async Task<IEnumerable<StreamsEventResponse.BatchItemFailure>> ProcessStreamAsync(DynamoDBEvent dynamoDbEvent, CancellationToken cancellationToken)
        {
            using var requestPayloadStream = new MemoryStream();

            // send event to target lambda function
            var request = new InvokeRequest
            {
                FunctionName = _streamHandlerLambdaSettings.FunctionName,
                InvocationType = InvocationType.RequestResponse,
                PayloadStream = requestPayloadStream,
            };

            _serializer.Serialize(dynamoDbEvent, request.PayloadStream);

            request.PayloadStream.Flush();
            request.PayloadStream.Seek(0, SeekOrigin.Begin);

            // Deserialize the awaited response into StreamsEventResponse and check for any failures. 
            var response = await _lambdaClient.InvokeAsync(request, cancellationToken);
            var streamsEventResponse = _serializer.Deserialize<StreamsEventResponse>(response.Payload);

            return streamsEventResponse.BatchItemFailures;
        }
    }
}
