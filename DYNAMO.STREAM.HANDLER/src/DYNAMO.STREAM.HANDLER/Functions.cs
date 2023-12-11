using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using DYNAMO.STREAM.HANDLER.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DYNAMO.STREAM.HANDLER;

public class Functions
{
    private readonly ILogger<Functions> _logger;
    private readonly IStreamHandler _streamHandler;

    public Functions()
    {
        var services = new ServiceCollection();
        // Configure your services here
        var startup = new Startup();
        startup.ConfigureServices(services);
        var provider = services.BuildServiceProvider();

        _logger = provider.GetRequiredService<ILogger<Functions>>();
        _streamHandler = provider.GetRequiredService<IStreamHandler>();
    }

    public StreamsEventResponse ProcessStream(DynamoDBEvent dynamoDbEvent, ILambdaContext context)
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
