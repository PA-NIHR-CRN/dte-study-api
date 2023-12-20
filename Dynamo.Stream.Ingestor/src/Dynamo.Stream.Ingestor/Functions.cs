using System.Diagnostics;
using Amazon.Lambda;
using Amazon.Lambda.Core;
using Dynamo.Stream.Handler.Entities;
using Dynamo.Stream.Ingestor.Repository;
using Dynamo.Stream.Ingestor.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dynamo.Stream.Ingestor.Extensions;
using Dynamo.Stream.Handler.Handlers;
using Polly;
using Dynamo.Stream.Handler.Mappers;
using Dynamo.Stream.Handler.Extensions;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Dynamo.Stream.Ingestor;

public class Functions
{
    private readonly IDynamoParticipantRepository _repository;
    private readonly ILogger<Functions> _logger;
    private readonly IDynamoDbEventService _dynamoDbEventService;
    private readonly IStreamHandler _streamHandler;
    private readonly ParticipantDbContext _dbContext;
    private readonly IParticipantMapper _participantMapper;

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
        _dbContext = provider.GetRequiredService<ParticipantDbContext>();
        _participantMapper = provider.GetRequiredService<IParticipantMapper>();
        provider.GetRequiredService<ParticipantDbContext>().Database.Migrate();
    }

    public async Task IngestParticipants()
    {
        var sw = Stopwatch.StartNew();
        var cts = new CancellationTokenSource();
        using (_logger.BeginScope("{FunctionName}", nameof(IngestParticipants)))
        {
            var participantItems = _repository.GetAllParticipantsAsAttributeMapsAsync(cts.Token);

            var participantCollection = new Dictionary<string, Participant>();

            await foreach (var item in participantItems)
            {
                var participant = _participantMapper.Map(item, new Participant());

                participantCollection.Add(item.PK(), participant);
            }

            var identifiers = participantCollection.Values
                .SelectMany(x => x.ParticipantIdentifiers)
                .DistinctBy(x => $"{x.IdentifierTypeId}-{x.Value}")
                .ToDictionary(x => $"{x.IdentifierTypeId}-{x.Value}", x => x);

            Dictionary<ParticipantIdentifier, Participant> idMap = new();

            foreach (var p in participantCollection.Values)
            {
                p.ParticipantIdentifiers = p.ParticipantIdentifiers
                    .Select(y => $"{y.IdentifierTypeId}-{y.Value}")
                    .Select(x => identifiers[x]).ToList();

                var participant = p.ParticipantIdentifiers
                    .Select(x => new { Found = idMap.TryGetValue(x, out var value), value })
                    .Where(x => x.Found)
                    .Select(x => x.value)
                    .FirstOrDefault();

                if (participant == null)
                {
                    participant = p;
                }

                foreach (var s in p.ParticipantIdentifiers)
                {
                    idMap[s] = participant;
                }
            }

            using (var transaction = _dbContext.Database.BeginTransaction())
            {

                _dbContext.Database.ExecuteSqlRaw("""
                SET autocommit=0; 
                SET unique_checks=0; 
                SET foreign_key_checks=0;
                """);

                _dbContext.Participants.AddRange(idMap.Select(x => x.Value).Distinct());

                _dbContext.SaveChanges();

                _dbContext.Database.ExecuteSqlRaw("""
                SET autocommit=1; 
                SET unique_checks=1; 
                SET foreign_key_checks=1;
                """);

                transaction.Commit();
            }

            _logger.LogInformation("Ingested all participants in {ElapsedTime}", sw.Elapsed);
        }
    }
}
