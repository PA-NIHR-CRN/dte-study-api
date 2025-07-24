using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using AWS.Lambda.Powertools.Idempotency;
using BPOR.Registration.Stream.Handler.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        var services = new ServiceCollection();
        IHostEnvironment hostEnvironment = new LambdaHostEnvironment(
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "BPOR.Registration.Stream.Handler",
            Directory.GetCurrentDirectory(), null);

        Startup.ConfigureServices(services, hostEnvironment);
        var provider = services.BuildServiceProvider();

        _logger = provider.GetRequiredService<ILogger<Functions>>();
        _streamHandler = provider.GetRequiredService<IStreamHandler>();
    }

    public StreamsEventResponse ProcessStream(DynamoDBEvent dynamoDbEvent, ILambdaContext context)
    {
        using (_logger.BeginScope("{FunctionName}", nameof(ProcessStream)))
        {
            Idempotency.RegisterLambdaContext(context);
            
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
