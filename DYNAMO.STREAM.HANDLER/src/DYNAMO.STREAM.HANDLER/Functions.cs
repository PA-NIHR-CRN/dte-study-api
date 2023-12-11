using Amazon.Lambda.Annotations;
using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using DYNAMO.STREAM.HANDLER.Handlers;
using Microsoft.Extensions.Logging;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DYNAMO.STREAM.HANDLER;

public class Functions
{
    [LambdaFunction]
    public StreamsEventResponse ProcessStream([FromServices] IStreamHandler streamHandler,
        [FromServices] ILogger<Functions> logger, DynamoDBEvent dynamoDbEvent)
    {
        using (logger.BeginScope("{FunctionName}", nameof(ProcessStream)))
        {
            logger.LogInformation("Number of records: {RecordsCount}", dynamoDbEvent.Records.Count);

            // AWS DynamoDb Stream handler is currently synchronous, but we want the library code
            // to remain async.
            var cts = new CancellationTokenSource();
            var failures = streamHandler.ProcessStreamAsync(dynamoDbEvent, cts.Token).Result;

            logger.LogInformation("DynamoDBEvent processing complete");

            return new StreamsEventResponse { BatchItemFailures = failures.ToList() };
        }
    }
}
