using System.Diagnostics;
using Amazon.Lambda;
using Amazon.Lambda.Core;
using Dynamo.Stream.Handler.Entities;
using Dynamo.Stream.Handler.Handlers;
using Dynamo.Stream.Ingestor.Repository;
using Dynamo.Stream.Ingestor.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dynamo.Stream.Ingestor.Extensions;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Dynamo.Stream.Ingestor;

public class Functions
{
    private readonly IDynamoParticipantRepository _repository;
    private readonly ILogger<Functions> _logger;
    private readonly IDynamoDbEventService _dynamoDbEventService;
    private readonly IStreamHandler _streamHandler;

    public Functions()
    {
        var services = new ServiceCollection();
        // Configure your services here
        Startup.ConfigureServices(services);
        var provider = services.BuildServiceProvider();

        _logger = provider.GetRequiredService<ILogger<Functions>>();
        _repository = provider.GetRequiredService<IDynamoParticipantRepository>();
        _dynamoDbEventService = provider.GetRequiredService<IDynamoDbEventService>();
        _streamHandler = provider.GetRequiredService<IStreamHandler>();
        provider.GetRequiredService<ParticipantDbContext>().Database.Migrate();
    }

    public async Task IngestParticipants()
    {
        const int batchSize = 1000;
        var sw = Stopwatch.StartNew();
        var participantsProcessed = 0;
        var cts = new CancellationTokenSource();
        using (_logger.BeginScope("{FunctionName}", nameof(IngestParticipants)))
        {
            var participants = _repository.GetAllParticipantsAsAttributeMapsAsync(cts.Token);
            var  participantsBatch = participants.Batch(batchSize);
            
            await foreach (var batch in participantsBatch)
            {
                var dynamoDbEvent = _dynamoDbEventService.CreateBatchEvent(batch);

                var errors = await _streamHandler.ProcessStreamAsync(dynamoDbEvent, cts.Token);

                if (errors.Any())
                {
                    _logger.LogError("Error processing event(s) {EventIds}", string.Join(", ", errors.Select(x => x.ItemIdentifier)));
                    throw new AmazonLambdaException($"Event(s) {string.Join(", ", errors.Select(x => x.ItemIdentifier))} failed to process.");
                }

                _logger.LogInformation("Sending batch of {BatchSize} participants to stream handler", batch.Count());
                
                participantsProcessed += batch.Count();
                
                _logger.LogInformation("Processed {ParticipantsProcessed} participants in {ElapsedTime}", participantsProcessed, sw.Elapsed);
                
            }
            _logger.LogInformation("Ingested all participants in {ElapsedTime}", sw.Elapsed);
        }
    }
}
