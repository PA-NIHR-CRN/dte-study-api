using System.Diagnostics;
using Amazon.Lambda.Core;
using Dynamo.Stream.Handler.Entities;
using Dynamo.Stream.Ingestor.Repository;
using Dynamo.Stream.Ingestor.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dynamo.Stream.Handler.Handlers;
using Dynamo.Stream.Handler.Mappers;
using Dynamo.Stream.Handler.Extensions;
using Amazon.DynamoDBv2.Model;
using Dynamo.Stream.Handler.Enums;

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
            var participantSource = _repository.GetAllParticipantsAsAttributeMapsAsync(cts.Token);

            var idMap = await ReadParticipants(participantSource);

            var participants = DeduplicateParticipants(idMap);

            _logger.LogInformation("{count} distinct identifiers.", idMap.Keys.Count);
            _logger.LogInformation("{count} distinct participants.", participants.Count);
            _logger.LogInformation("{count} distinct source references (PKs).", participants.SelectMany(x => x.SourceReferences).DistinctBy(x => x.Pk).Count());

            _logger.LogInformation("Processed all participants in {ElapsedTime}", sw.Elapsed);

            SaveParticipants(participants);

            _logger.LogInformation("Ingested all participants in {ElapsedTime}", sw.Elapsed);
        }
    }

    private async Task<Dictionary<Guid, List<Participant>>> ReadParticipants(IAsyncEnumerable<Dictionary<string, AttributeValue>> participantSource)
    {
        Dictionary<Guid, List<Participant>> idMap = new();

        var pks = new List<string>();

        int itemsRead = 0;
        await foreach (var item in participantSource)
        {
            var participant = _participantMapper.Map(item, new Participant());

            pks.Add(item.PK());

            MapParticipantIdentifersToParticipants(idMap, participant);

            itemsRead++;
        }
        _logger.LogInformation("{count} items read.", itemsRead);

        _logger.LogInformation("PK count {pkcount}", idMap.Values.SelectMany(x => x).SelectMany(x => x.SourceReferences).Select(x => x.Pk).Distinct().Count());

        EnsureAllSourceItemsProcessed(idMap, pks);

        return idMap;
    }

    /// <summary>
    /// Maintains a one to many mapping between an identifier and the participant that share that identifier.
    /// </summary>
    /// <param name="idMap">Identifier to participants mapping.</param>
    /// <param name="participant">Participant to add to the mapping.</param>
    private void MapParticipantIdentifersToParticipants(Dictionary<Guid, List<Participant>> idMap, Participant participant)
    {
        if (!participant.ParticipantIdentifiers.Any())
        {
            _logger.LogWarning("Participant {pk} has no participant identifiers.", participant.SourceReferences.First().Pk);

            AddToMapping(idMap, participant, Guid.Empty);
        }

        foreach (var id in participant.ParticipantIdentifiers)
        {
            AddToMapping(idMap, participant, id.Value);
        }
    }

    private static void AddToMapping(Dictionary<Guid, List<Participant>> idMap, Participant participant, Guid id)
    {
        if (!idMap.ContainsKey(id))
        {
            idMap[id] = new List<Participant> { participant };
        }
        else
        {
            idMap[id].Add(participant);
        }
    }

    private void EnsureAllSourceItemsProcessed(Dictionary<Guid, List<Participant>> idMap, List<string> pks)
    {
        var missing = pks.Except(idMap.Values.SelectMany(x => x)
            .SelectMany(x => x.SourceReferences)
            .Select(x => x.Pk)
            .Distinct());

        if (missing.Any())
        {
            _logger.LogError("Missing source items: {missing}", string.Join(", ", missing));
        }
    }

    /// <summary>
    /// Combines multiple participants that share any identifiers into a single participant item,
    /// </summary>
    /// <param name="idMap">Mapping between each identifier and the participants that share that identifier.</param>
    /// <returns>A list of deduplicated participants with combined identifiers and source references.</returns>
    private static List<Participant> DeduplicateParticipants(Dictionary<Guid, List<Participant>> idMap)
    {
        var uniqueParticipants = new List<Participant>();

        var map = new Dictionary<Guid, Participant>();
        var identifierTypePrecendence = new List<int> { (int)IdentifierTypes.Deleted, (int)IdentifierTypes.ParticipantId, (int)IdentifierTypes.NhsId };

        foreach (var item in idMap.Values)
        {
            var primaryParticipant = item
                .OrderBy(x => x.ParticipantIdentifiers.Select(y => identifierTypePrecendence.IndexOf(y.IdentifierTypeId)).DefaultIfEmpty(0).Max())
                .OrderBy(x => x.CreatedAt)
                .Last();


            // Combine identifiers of all the duplicate participants.
            primaryParticipant.ParticipantIdentifiers = item
                                        .SelectMany(x => x.ParticipantIdentifiers)
                                        .DistinctBy(x => $"{x.IdentifierTypeId}-{x.Value}")
                                        .ToList();

            // Combine source references of all the duplicates.
            primaryParticipant.SourceReferences = item.SelectMany(x => x.SourceReferences).ToList();

            var existingParticipant = primaryParticipant.ParticipantIdentifiers.Select(x => map.GetValueOrDefault(x.Value)).Where(x => x != null).Distinct().SingleOrDefault();

            if (existingParticipant == null)
            {
                existingParticipant = primaryParticipant;
                uniqueParticipants.Add(existingParticipant);
            }
            else
            {
                existingParticipant.ParticipantIdentifiers = existingParticipant.ParticipantIdentifiers.UnionBy(primaryParticipant.ParticipantIdentifiers, x => $"{x.IdentifierTypeId}-{x.Value}").ToList();

                existingParticipant.SourceReferences = existingParticipant.SourceReferences.UnionBy(primaryParticipant.SourceReferences, x => x.Pk).ToList();
            }

            foreach (var i in existingParticipant.ParticipantIdentifiers)
            {
                if (map.ContainsKey(i.Value) && map[i.Value] != existingParticipant)
                {
                    throw new DuplicateItemException($"Duplicate item '{i.Value}'");
                }

                map[i.Value] = existingParticipant;
            }
        }

        return uniqueParticipants;
    }

    private void SaveParticipants(List<Participant> participantCollection)
    {
        _logger.LogInformation("Adding to database...");

        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            _dbContext.Database.ExecuteSqlRaw(@"SET autocommit=0; SET unique_checks=0; SET foreign_key_checks=0;");

            _dbContext.Participants.AddRange(participantCollection);

            _logger.LogInformation("Saving...");
            _dbContext.SaveChanges();

            _dbContext.Database.ExecuteSqlRaw(@"SET autocommit=1; SET unique_checks=1; SET foreign_key_checks=1;");

            _logger.LogInformation("Committing transaction...");
            transaction.Commit();
        }
    }
}
