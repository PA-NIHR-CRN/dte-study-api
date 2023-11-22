using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using DYNAMO.STREAM.HANDLER.Contracts;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DYNAMO.STREAM.HANDLER;

public class Functions
{
    private readonly IStreamHandler _streamHandler;

    public Functions(IStreamHandler streamHandler) => _streamHandler = streamHandler;

    [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
    public async Task ProcessStream(DynamoDBEvent dynamoDbEvent, ILambdaContext context)
    {
        context.Logger.LogInformation($"Beginning to process {dynamoDbEvent.Records.Count} records...");
        await _streamHandler.ProcessStream(dynamoDbEvent);
        context.Logger.LogInformation("Stream processing complete.");
    }
}
