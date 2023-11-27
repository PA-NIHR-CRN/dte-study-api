using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using DYNAMO.STREAM.HANDLER.Contracts;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DYNAMO.STREAM.HANDLER;

public class Functions
{
    private readonly IStreamHandler _streamHandler;
    private readonly IDataIngestor _dataIngestor;

    public Functions(IStreamHandler streamHandler, IDataIngestor dataIngestor)
    {
        _streamHandler = streamHandler;
        _dataIngestor = dataIngestor;
    }

    [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
    public async Task ProcessStream(DynamoDBEvent dynamoDbEvent, ILambdaContext context)
    {
        // TODO: use our an injected ILogger here.
        // Add a log scope with any useful information from the context / event.
        context.Logger.LogInformation($"Beginning to process {dynamoDbEvent.Records.Count} records...");
        await _streamHandler.ProcessStream(dynamoDbEvent);
        context.Logger.LogInformation("Stream processing complete.");
    }
    
    [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
    public async Task IngestData(ILambdaContext context)
    {
        // TODO: logging as above
        context.Logger.LogInformation("Beginning to ingest data...");
        await _dataIngestor.IngestDataAsync();
        context.Logger.LogInformation("Data ingestion complete.");
    }
}
