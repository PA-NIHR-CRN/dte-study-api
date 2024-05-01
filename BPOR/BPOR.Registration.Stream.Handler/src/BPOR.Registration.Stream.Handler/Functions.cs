using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using BPOR.Registration.Stream.Handler.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NIHR.Infrastructure.Configuration;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace BPOR.Registration.Stream.Handler;

public class Functions
{
    private readonly ILogger<Functions> _logger;
    private readonly IStreamHandler _streamHandler;

    public Functions()
    {
        var builder = Lambda.CreateBuilder("BPOR.Registration.Stream.Handler");

        builder.AddNihrConfiguration();
        builder.ConfigureNihrLogging();

        Startup.ConfigureServices(builder);

        var host = builder.Build();

        _logger = host.Services.GetRequiredService<ILogger<Functions>>();
        _streamHandler = host.Services.GetRequiredService<IStreamHandler>();
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
