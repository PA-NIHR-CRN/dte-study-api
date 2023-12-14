using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda;
using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Dynamo.Stream.Handler.Entities;
using Dynamo.Stream.Handler.Extensions;
using Dynamo.Stream.Handler.Handlers;
using Dynamo.Stream.Ingestor.Repository;
using Dynamo.Stream.Ingestor.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
        var cts = new CancellationTokenSource();
        using (_logger.BeginScope("{FunctionName}", nameof(IngestParticipants)))
        {
            var participants = await _repository.GetAllParticipantsAsAttributeMapsAsync(cts.Token);
            foreach (var participant in participants)
            {
                DynamoDBEvent streamEvent;

                var pk = participant.PK();

                if (pk.StartsWith("DELETED#"))
                {
                    streamEvent = _dynamoDbEventService.CreateEvent(OperationType.REMOVE, oldImage: participant);
                }
                else
                {
                    streamEvent = _dynamoDbEventService.CreateEvent(OperationType.INSERT, newImage: participant);
                }
                
                var errors = await _streamHandler.ProcessStreamAsync(streamEvent, cts.Token);
                if (errors.Any())
                {
                    _logger.LogError("{@errors}", errors);
                    throw new AmazonLambdaException($"Event(s) {string.Join(", ", errors.Select(x => x.ItemIdentifier))} failed to process.");
                }

                _logger.LogInformation("Sent participant {ParticipantParticipantId} to target lambda function",
                    participant["PK"].S);
            }
        }
    }
}