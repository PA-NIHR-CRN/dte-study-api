using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.DynamoDBEvents;
using Dynamo.Stream.Handler.Entities;
using Dynamo.Stream.Handler.Extensions;
using Dynamo.Stream.Handler.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Amazon.Lambda.DynamoDBEvents.StreamsEventResponse;

namespace Dynamo.Stream.Handler.Handlers;

public class StreamHandler : IStreamHandler
{
    private readonly ParticipantDbContext _dbContext;
    private readonly ILogger<StreamHandler> _logger;
    private readonly IParticipantMapper _participantMapper;
    private Dictionary<string, int> _existingRecordsIds = new Dictionary<string, int>();

    public StreamHandler(ParticipantDbContext dbContext, ILogger<StreamHandler> logger,
        IParticipantMapper participantMapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _participantMapper = participantMapper;
    }

    public async Task<IEnumerable<BatchItemFailure>> ProcessStreamAsync(DynamoDBEvent dynamoDbEvent, CancellationToken cancellationToken)
    {
        var failures = new List<BatchItemFailure>();
        // TODO: how do we handle out of order events if they are indeed out of order?
        string currentRecordSequenceNumber;

        var identifiers = dynamoDbEvent.Records.SelectMany(x => _participantMapper.ExtractIdentifiers(x.Dynamodb.NewImage)).Union(
            dynamoDbEvent.Records.SelectMany(x => _participantMapper.ExtractIdentifiers(x.Dynamodb.OldImage))).Select(x => x.Value).Distinct();

        _existingRecordsIds = _dbContext.ParticipantIdentifiers
            .Where(x => identifiers.Contains(x.Value))
            .Select(x => new { x.Pk, x.ParticipantId })
            .ToList()
            .DistinctBy(x => x.Pk)
            .ToDictionary(x => x.Pk, x => x.ParticipantId);

        foreach (var record in dynamoDbEvent.Records)
        {
            currentRecordSequenceNumber = record.Dynamodb.SequenceNumber;

            using (_logger.BeginScope("{EventId}, {SequenceNumber}", record.EventID, currentRecordSequenceNumber))
            {
                try
                {
                    _logger.LogInformation("Processing record {SequenceNumber}, ApproximateCreationDateTime: {ApproximateCreationDateTime}", currentRecordSequenceNumber, record.Dynamodb.ApproximateCreationDateTime);

                    _logger.LogDebug("AwsRegion: {AwsRegion}, EventID: {EventID}, EventName: {EventName}, EventSource: {EventSource}, EventSourceArn: {EventSourceArn}, EventVersion: {EventVersion}, UserIdentity: {@UserIdentity}, SizeBytes: {SizeBytes}, StreamViewType: {StreamViewType}",
                                           record.AwsRegion,
                                           record.EventID,
                                           record.EventName,
                                           record.EventSource,
                                           record.EventSourceArn,
                                           record.EventVersion,
                                           record.UserIdentity,
                                           record.Dynamodb.SizeBytes,
                                           record.Dynamodb.StreamViewType
                                           );

                    _logger.LogTrace("{@record}", record);

                    await ProcessRecordAsync(record, cancellationToken);

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    _logger.LogInformation("Record processing complete");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                    failures.Add(new BatchItemFailure { ItemIdentifier = currentRecordSequenceNumber });

                    // If there is one failure the whole batch is retried, exit early here.
                    return failures;
                }
            }
        }

        return failures;
    }

    private async Task ProcessRecordAsync(DynamoDBEvent.DynamodbStreamRecord record, CancellationToken cancellationToken)
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

    private async Task ProcessInsertAsync(DynamoDBEvent.DynamodbStreamRecord record, CancellationToken cancellationToken)
    {
        await InsertAsync(record.Dynamodb.NewImage, cancellationToken);
    }

    private async Task<Participant> InsertAsync(Dictionary<string, AttributeValue> image, CancellationToken cancellationToken)
    {
        Participant? targetParticipant = null;
        if (_existingRecordsIds.ContainsKey(image.PK()))
        {
            targetParticipant = await _dbContext.Participants.Where(x => x.Id == _existingRecordsIds[image.PK()])
                                                .ForUpdate()
                                                .SingleOrDefaultAsync(cancellationToken);
        }

        if (targetParticipant == null)
        {
            // No linked participant exists, create a new one.
            targetParticipant = _dbContext.Participants.Add(new Participant()).Entity;
        }

        return _participantMapper.Map(image, targetParticipant);
    }

    private async Task ProcessModifyAsync(DynamoDBEvent.DynamodbStreamRecord record, CancellationToken cancellationToken)
    {
        Participant? targetParticipant = null;
        if (_existingRecordsIds.ContainsKey(record.Dynamodb.NewImage.PK()))
        {
            targetParticipant = await _dbContext.Participants.Where(x => x.Id == _existingRecordsIds[record.Dynamodb.NewImage.PK()])
                                                .ForUpdate()
                                                .SingleOrDefaultAsync(cancellationToken);
        }

        if (targetParticipant == null)
        {
            targetParticipant = await InsertAsync(record.Dynamodb.OldImage, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        _participantMapper.Map(record.Dynamodb.NewImage, targetParticipant);
    }

    private async Task ProcessRemoveAsync(DynamoDBEvent.DynamodbStreamRecord record,
        CancellationToken cancellationToken)
    {

        Participant? targetParticipant = null;
        if (_existingRecordsIds.ContainsKey(record.Dynamodb.OldImage.PK()))
        {
            targetParticipant = await _dbContext.Participants.Where(x => x.Id == _existingRecordsIds[record.Dynamodb.OldImage.PK()])
                                                .Include(x => x.ParticipantIdentifiers)
                                                .SingleOrDefaultAsync(cancellationToken);
        }

        if (targetParticipant == null)
        {
            targetParticipant = await InsertAsync(record.Dynamodb.OldImage, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        var identifiers = _participantMapper.ExtractIdentifiers(record.Dynamodb.OldImage);


        // TODO: are we removing the Participant here, or just the ParticipantIdentifer?
        // Only delete the Participant if all participant identifiers have also been deleted.
        var idsToRemove =
            targetParticipant.ParticipantIdentifiers.Where(x => identifiers.Select(y => y.Value).Contains(x.Value));

        _dbContext.ParticipantIdentifiers.RemoveRange(idsToRemove);

        if (targetParticipant.ParticipantIdentifiers.Except(idsToRemove).All(x => x.IsDeleted))
        {
            _dbContext.Participants.Remove(targetParticipant);
        }
    }
}
