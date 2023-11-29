using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using DYNAMO.STREAM.HANDLER.Handlers;
using Microsoft.Extensions.Logging;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DYNAMO.STREAM.HANDLER;

public class Functions
{
    private readonly IStreamHandler _streamHandler;
    private readonly ILogger<Functions> _logger;

    public Functions(IStreamHandler streamHandler, ILogger<Functions> logger)
    {
        _streamHandler = streamHandler;
        _logger = logger;
    }

    [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
    public StreamsEventResponse ProcessStream(DynamoDBEvent dynamoDbEvent)
    {
        using (_logger.BeginScope("{FunctionName}", nameof(ProcessStream)))
        {
            _logger.LogInformation("Number of records: {RecordsCount}", dynamoDbEvent.Records.Count);

            // AWS DynamoDb Stream handler is currently synchronous, but we want the library code
            // to remain async.
            var cts = new CancellationTokenSource();
            var failures = _streamHandler.ProcessStreamAsync(dynamoDbEvent, cts.Token).Result;

            _logger.LogInformation("DynamoDBEvent processing complete");

            return new StreamsEventResponse { BatchItemFailures = failures.ToList() };
        }
    }
}
