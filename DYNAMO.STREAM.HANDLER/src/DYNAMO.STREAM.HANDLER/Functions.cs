using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using DYNAMO.STREAM.HANDLER.Contracts;
using Microsoft.Extensions.Logging;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DYNAMO.STREAM.HANDLER;

public class Functions
{
    private readonly IStreamHandler _streamHandler;
    private readonly IDataIngestor _dataIngestor;
    private readonly ILogger<Functions> _logger;

    public Functions(IStreamHandler streamHandler, IDataIngestor dataIngestor, ILogger<Functions> logger)
    {
        _streamHandler = streamHandler;
        _dataIngestor = dataIngestor;
        _logger = logger;
    }

    [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
    public async Task ProcessStream(DynamoDBEvent dynamoDbEvent, CancellationToken cancellationToken)
    {
        // TODO: should this pattern be followed throughout the application?
        using (_logger.BeginScope(nameof(ProcessStream)))
        {
            _logger.LogInformation("Beginning to process {RecordsCount} records...", dynamoDbEvent.Records.Count);
            await _streamHandler.ProcessStreamAsync(dynamoDbEvent, cancellationToken);
            _logger.LogInformation("Stream processing complete");
        }
    }

    [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
    public async Task IngestData(CancellationToken cancellationToken)
    {
        using (_logger.BeginScope(nameof(ProcessStream)))
        {
            _logger.LogInformation("Beginning to ingest data...");
            await _dataIngestor.IngestDataAsync(cancellationToken);
            _logger.LogInformation("Data ingestion complete.");
        }
    }
}
