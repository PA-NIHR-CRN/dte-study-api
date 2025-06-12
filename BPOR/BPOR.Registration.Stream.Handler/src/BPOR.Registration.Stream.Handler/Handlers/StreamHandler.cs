using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.DynamoDBEvents;
using BPOR.Domain.Entities;
using BPOR.Registration.Stream.Handler.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Amazon.Lambda.DynamoDBEvents.StreamsEventResponse;

namespace BPOR.Registration.Stream.Handler.Handlers;

public class StreamHandler(
    ParticipantDbContext participantDbContext,
    ILogger<StreamHandler> logger,
    IParticipantMapper participantMapper)
    : IStreamHandler
{
    public async Task<IEnumerable<BatchItemFailure>> ProcessStreamAsync(DynamoDBEvent dynamoDbEvent,
        CancellationToken cancellationToken)
    {
        // participantDbContext.ThrowIfInMaintenanceMode();

        var failures = new List<BatchItemFailure>();
        // TODO: how do we handle out of order events if they are indeed out of order?
        string currentRecordSequenceNumber;

        foreach (var record in dynamoDbEvent.Records)
        {
            currentRecordSequenceNumber = record.Dynamodb.SequenceNumber;

            using (logger.BeginScope("{EventId}, {SequenceNumber}", record.EventID, currentRecordSequenceNumber))
            {

                if (record.EventName == OperationType.MODIFY && record.Dynamodb.NewImage.TryGetValue("IsStage2CompleteUtcBackfilled", out var NewIsStage2CompleteUtcBackfilled)
                    && !record.Dynamodb.OldImage.TryGetValue("IsStage2CompleteUtcBackfilled", out var OldIsStage2CompleteUtcBackfilled))
                {
                    logger.LogInformation("Skipping new IsStage2CompleteUtcBackfilled modification");
                    continue;
                }

                try
                {
                    logger.LogInformation(
                        "Processing record {SequenceNumber}, ApproximateCreationDateTime: {ApproximateCreationDateTime}",
                        currentRecordSequenceNumber, record.Dynamodb.ApproximateCreationDateTime);

                    logger.LogDebug(
                        "AwsRegion: {AwsRegion}, EventID: {EventID}, EventName: {EventName}, EventSource: {EventSource}, EventSourceArn: {EventSourceArn}, EventVersion: {EventVersion}, UserIdentity: {@UserIdentity}, SizeBytes: {SizeBytes}, StreamViewType: {StreamViewType}",
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

                    logger.LogTrace("{@Record}", record);

                    await ProcessRecordAsync(record, cancellationToken);

                    await participantDbContext.SaveChangesAsync(cancellationToken);

                    logger.LogInformation("Record processing complete");
                }
                catch (Exception e)
                {
                    logger.LogError(e, e.Message);
                    failures.Add(new BatchItemFailure { ItemIdentifier = currentRecordSequenceNumber });

                    // If there is one failure the whole batch is retried, exit early here.
                    // Support 'Report batch item failures: Yes'
                    // return failures;

                    // Do not support partial batch failure.
                    // See 'Report batch item failures: No'
                    throw;
                }
            }
        }

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
        var identifiers = participantMapper.ExtractIdentifiers(image);


        var targetParticipant = await participantDbContext.GetParticipantByLinkedIdentifiers(identifiers)
            .ForUpdate()
            .SingleOrDefaultAsync(cancellationToken);


        if (targetParticipant == null)
        {
            // No linked participant exists, create a new one.
            targetParticipant = participantDbContext.Participants.Add(new Participant()).Entity;
        }

        return await participantMapper.Map(image, targetParticipant, cancellationToken);
    }

    private async Task ProcessModifyAsync(DynamoDBEvent.DynamodbStreamRecord record,
        CancellationToken cancellationToken)
    {
        var identifiers = participantMapper.ExtractIdentifiers(record.Dynamodb.NewImage);
        var participant = await participantDbContext.GetParticipantByLinkedIdentifiers(identifiers)
            .ForUpdate()
            .SingleOrDefaultAsync(cancellationToken);

        if (participant == null)
        {
            participant = await InsertAsync(record.Dynamodb.OldImage, cancellationToken);
            await participantDbContext.SaveChangesAsync(cancellationToken);
        }

        await participantMapper.Map(record.Dynamodb.NewImage, participant, cancellationToken);
    }

    private async Task ProcessRemoveAsync(DynamoDBEvent.DynamodbStreamRecord record,
        CancellationToken cancellationToken)
    {
        var identifiers = participantMapper.ExtractIdentifiers(record.Dynamodb.OldImage);
        var participant = await participantDbContext.GetParticipantByLinkedIdentifiers(identifiers)
            .IgnoreQueryFilters()
            .Include(x => x.ParticipantIdentifiers)
            .Include(x => x.ContactMethodId)
            .SingleOrDefaultAsync(cancellationToken);

        if (participant == null)
        {
            participant = await InsertAsync(record.Dynamodb.OldImage, cancellationToken);
            await participantDbContext.SaveChangesAsync(cancellationToken);
        }

        // Remove participant contact method record
        participantDbContext.ParticipantContactMethod.RemoveRange(participant.ContactMethodId);

        // TODO: are we removing the Participant here, or just the ParticipantIdentifer?
        // Only delete the Participant if all participant identifiers have also been deleted.
        var idsToRemove =
            participant.ParticipantIdentifiers.Where(x => identifiers.Select(y => y.Value).Contains(x.Value));

        participantDbContext.ParticipantIdentifiers.RemoveRange(idsToRemove);

        if (participant.ParticipantIdentifiers.Except(idsToRemove).All(x => x.IsDeleted))
        {
            participantDbContext.Participants.Remove(participant);
        }
    }
}
