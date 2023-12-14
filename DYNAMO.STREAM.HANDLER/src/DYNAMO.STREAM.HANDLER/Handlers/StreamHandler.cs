using Amazon.DynamoDBv2;
using Amazon.Lambda.DynamoDBEvents;
using Dynamo.Stream.Handler.Entities;
using Dynamo.Stream.Handler.Extensions;
using Dynamo.Stream.Handler.Mappers;
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

    public async Task<IEnumerable<BatchItemFailure>> ProcessStreamAsync(DynamoDBEvent dynamoDbEvent, CancellationToken cancellationToken)
    {
        var failures = new List<BatchItemFailure>();
        // TODO: how do we handle out of order events if they are indeed out of order?
        string currentRecordSequenceNumber;

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
        var pk = record.Dynamodb.NewImage.PK();

        if (pk.StartsWith("DELETED#", StringComparison.InvariantCultureIgnoreCase))
        {
            return;
        }

        var identifiers = _participantMapper.ExtractIdentifiers(record.Dynamodb.NewImage);

        var targetParticipant = await _dbContext.GetParticipantByLinkedIdentifiersAsync(identifiers, cancellationToken);

        if (targetParticipant == null)
        {
            // No linked participant exists, create a new one.
            targetParticipant = new Participant();
            await _dbContext.Participants.AddAsync(targetParticipant, cancellationToken);
        }

        _participantMapper.Map(record.Dynamodb.NewImage, targetParticipant);
    }

    private async Task ProcessModifyAsync(DynamoDBEvent.DynamodbStreamRecord record, CancellationToken cancellationToken)
    {
        var pk = record.Dynamodb.NewImage.PK();
        var participant = await _dbContext.GetParticipantByPkAsync(pk, cancellationToken);

        if (participant == null)
        {
            throw new KeyNotFoundException($"Participant with PK '{pk}' not found.");
        }

        _participantMapper.Map(record.Dynamodb.NewImage, participant);
    }

    private async Task ProcessRemoveAsync(DynamoDBEvent.DynamodbStreamRecord record, CancellationToken cancellationToken)
    {
        var pk = record.Dynamodb.OldImage.PK();
        var participant = await _dbContext.GetParticipantByPkAsync(pk, cancellationToken);

        if (participant == null)
        {
            throw new KeyNotFoundException($"Participant with PK '{pk}' not found.");
        }

        _dbContext.Participants.Remove(participant);
    }
}
