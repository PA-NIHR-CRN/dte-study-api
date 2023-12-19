using System.Collections.Concurrent;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.DynamoDBEvents;
using Dynamo.Stream.Handler.Entities;
using Dynamo.Stream.Handler.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static Amazon.Lambda.DynamoDBEvents.StreamsEventResponse;

namespace Dynamo.Stream.Handler.Handlers;

public class StreamHandler : IStreamHandler
{
    private readonly ParticipantDbContext _dbContext;
    private readonly ILogger<StreamHandler> _logger;
    private readonly IParticipantMapper _participantMapper;

    public StreamHandler(ParticipantDbContext dbContext, ILogger<StreamHandler> logger,
        IParticipantMapper participantMapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _participantMapper = participantMapper;
    }

    public async Task<IEnumerable<BatchItemFailure>> ProcessStreamAsync(DynamoDBEvent dynamoDbEvent,
        IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        var failures = new ConcurrentBag<BatchItemFailure>();
        var tasks = dynamoDbEvent.Records.Select(record => Task.Run(async () =>
        {
            // Create a new scope for each task
            using var scope = serviceProvider.CreateScope();
            var scopedDbContext = scope.ServiceProvider.GetRequiredService<ParticipantDbContext>();

            try
            {
                using (_logger.BeginScope("{EventId}, {SequenceNumber}", record.EventID,
                           record.Dynamodb.SequenceNumber))
                {
                    _logger.LogInformation(
                        "Processing record {SequenceNumber}, ApproximateCreationDateTime: {ApproximateCreationDateTime}",
                        record.Dynamodb.SequenceNumber, record.Dynamodb.ApproximateCreationDateTime);
                    _logger.LogTrace("{@record}", record);

                    await ProcessRecordAsync(record, cancellationToken);
                    await scopedDbContext.SaveChangesAsync(cancellationToken);

                    _logger.LogInformation("Record processing complete");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                failures.Add(new BatchItemFailure { ItemIdentifier = record.Dynamodb.SequenceNumber });
            }
        }, cancellationToken));

        await Task.WhenAll(tasks);

        return failures;
    }

    private async Task ProcessRecordAsync(DynamoDBEvent.DynamodbStreamRecord record,
        CancellationToken cancellationToken)
    {
        if (record.EventName == OperationType.INSERT)
        {
            await ProcessInsertAsync(record, cancellationToken);
        }
        else if (record.EventName == OperationType.MODIFY)
        {
            await ProcessModifyAsync(record, cancellationToken);
        }
        else if (record.EventName == OperationType.REMOVE)
        {
            await ProcessRemoveAsync(record, cancellationToken);
        }
        else
        {
            throw new NotSupportedException($"Unknown Event Name {record.EventName}");
        }
    }

    private async Task ProcessInsertAsync(DynamoDBEvent.DynamodbStreamRecord record,
        CancellationToken cancellationToken)
    {
        await InsertAsync(record.Dynamodb.NewImage, cancellationToken);
    }

    private async Task<Participant> InsertAsync(Dictionary<string, AttributeValue> image,
        CancellationToken cancellationToken)
    {
        var identifiers = _participantMapper.ExtractIdentifiers(image);

        var targetParticipant = await _dbContext.GetParticipantByLinkedIdentifiers(identifiers)
            .ForUpdate()
            .SingleOrDefaultAsync(cancellationToken);


        if (targetParticipant == null)
        {
            // No linked participant exists, create a new one.
            targetParticipant = _dbContext.Participants.Add(new Participant()).Entity;
        }

        return _participantMapper.Map(image, targetParticipant);
    }

    private async Task ProcessModifyAsync(DynamoDBEvent.DynamodbStreamRecord record,
        CancellationToken cancellationToken)
    {
        var identifiers = _participantMapper.ExtractIdentifiers(record.Dynamodb.NewImage);
        var participant = await _dbContext.GetParticipantByLinkedIdentifiers(identifiers)
            .ForUpdate()
            .SingleOrDefaultAsync(cancellationToken);

        if (participant == null)
        {
            participant = await InsertAsync(record.Dynamodb.OldImage, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        _participantMapper.Map(record.Dynamodb.NewImage, participant);
    }

    private async Task ProcessRemoveAsync(DynamoDBEvent.DynamodbStreamRecord record,
        CancellationToken cancellationToken)
    {
        var identifiers = _participantMapper.ExtractIdentifiers(record.Dynamodb.OldImage);
        var participant = await _dbContext.GetParticipantByLinkedIdentifiers(identifiers)
            .Include(x => x.ParticipantIdentifiers)
            .SingleOrDefaultAsync(cancellationToken);

        if (participant == null)
        {
            participant = await InsertAsync(record.Dynamodb.OldImage, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        // TODO: are we removing the Participant here, or just the ParticipantIdentifer?
        // Only delete the Participant if all participant identifiers have also been deleted.
        var idsToRemove =
            participant.ParticipantIdentifiers.Where(x => identifiers.Select(y => y.Value).Contains(x.Value));

        _dbContext.ParticipantIdentifiers.RemoveRange(idsToRemove);

        if (participant.ParticipantIdentifiers.Except(idsToRemove).All(x => x.IsDeleted))
        {
            _dbContext.Participants.Remove(participant);
        }
    }
}
