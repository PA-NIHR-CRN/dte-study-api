using Amazon.Lambda;
using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Amazon.Lambda.Model;
using DYNAMO.STREAM.HANDLER.Entities;
using DYNAMO.STREAM.INGESTOR.Repository;
using DYNAMO.STREAM.INGESTOR.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DYNAMO.STREAM.INGESTOR;

public class Functions
{
    private readonly IDynamoParticipantRepository _repository;
    private readonly ILogger<Functions> _logger;
    private readonly IAmazonLambda _lambdaClient;
    private readonly IDynamoDbEventService _dynamoDbEventService;
    private readonly ILambdaSerializer _serializer;

    public Functions()
    {
        var services = new ServiceCollection();
        // Configure your services here
        var startup = new Startup();
        startup.ConfigureServices(services);
        var provider = services.BuildServiceProvider();

        _logger = provider.GetRequiredService<ILogger<Functions>>();
        _repository = provider.GetRequiredService<IDynamoParticipantRepository>();
        _lambdaClient = provider.GetRequiredService<IAmazonLambda>();
        _dynamoDbEventService = provider.GetRequiredService<IDynamoDbEventService>();
        _serializer = provider.GetRequiredService<ILambdaSerializer>();
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
                try
                {
                    // send event to target lambda function
                    var invokeRequest = new InvokeRequest
                    {
                        FunctionName =
                            "arn:aws:lambda:eu-west-2:841171564302:function:crnccd-lambda-dev-dte-participant-stream",
                        InvocationType = InvocationType.RequestResponse,
                        PayloadStream = new MemoryStream()
                    };

                    _serializer.Serialize(_dynamoDbEventService.CreateParticipantInsertEvent(participant), invokeRequest.PayloadStream);

                    invokeRequest.PayloadStream.Flush();
                    invokeRequest.PayloadStream.Seek(0, SeekOrigin.Begin);

                    // Deserialize the awaited response into StreamsEventResponse and check for any failures. 
                    var response = await _lambdaClient.InvokeAsync(invokeRequest, cts.Token);
                    var streamsResponse = _serializer.Deserialize<StreamsEventResponse>(response.Payload);
                    
                    if (streamsResponse.BatchItemFailures.Any())
                    {
                        _logger.LogError("Failed to send participant {ParticipantParticipantId} to target lambda function",
                            participant["PK"].S);
                        throw new AmazonLambdaException("Failed to send participant to target lambda function");
                    }

                    _logger.LogInformation("Sent participant {ParticipantParticipantId} to target lambda function",
                        participant["PK"].S);
                }
                catch (AmazonLambdaException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    _logger.LogError(e,
                        "Failed to send participant {ParticipantParticipantId} to target lambda function",
                        participant["PK"].S);
                    throw;
                }
            }
        }
    }
}
