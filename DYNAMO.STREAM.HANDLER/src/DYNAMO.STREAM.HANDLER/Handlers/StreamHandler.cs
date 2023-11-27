using Amazon.DynamoDBv2;
using Amazon.Lambda.DynamoDBEvents;
using Dte.Common.Contracts;
using DYNAMO.STREAM.HANDLER.Contracts;
using DYNAMO.STREAM.HANDLER.Entities;
using DYNAMO.STREAM.HANDLER.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;

namespace DYNAMO.STREAM.HANDLER.Handlers;

public class StreamHandler : IStreamHandler
{
    private readonly ParticipantDbContext _dbContext;
    private readonly ILogger<StreamHandler> _logger;
    private readonly IParticipantMapper _participantMapper;
    private readonly IClock _clock;
    private readonly IAsyncPolicy _retryPolicy;

    public StreamHandler(ParticipantDbContext dbContext, IAsyncPolicy retryPolicy, ILogger<StreamHandler> logger,
        IParticipantMapper participantMapper, IClock clock)
    {
        _dbContext = dbContext;
        _retryPolicy = retryPolicy;
        _logger = logger;
        _participantMapper = participantMapper;
        _clock = clock;
    }

    public async Task ProcessStream(DynamoDBEvent dynamoDbEvent)
    {
        foreach (var record in dynamoDbEvent.Records)
        {
            _logger.LogInformation("**** Processing {EventType} Event ID: {RecordEventId}", record.EventName,
                record.EventID);

            try
            {
                await _retryPolicy.ExecuteAsync(async () => await ProcessRecord(record));
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "**** Error processing Event ID: {RecordEventId}", record.EventID);
            }

            _logger.LogInformation("****  Finished processing Event ID: {RecordEventId}", record.EventID);
        }
    }

    private Task ProcessRecord(DynamoDBEvent.DynamodbStreamRecord record)
    {
        if (record.EventName == OperationType.INSERT)
        {
            return ProcessInsert(record);
        }
        else if (record.EventName == OperationType.MODIFY)
        {
            return ProcessModify(record);
        }
        else if (record.EventName == OperationType.REMOVE)
        {
            return ProcessRemove(record);
        }
        else
        {
            //TODO: Never throw a base Exception. Always throw something specific.
            throw new NotImplementedException($"Unknown Event Name {record.EventName}");
        }
    }

    private Task ProcessInsert(DynamoDBEvent.DynamodbStreamRecord record)
    {
        // check if the record sk is DELETED# and return as this is a newly created DELETE record should this be done before mapping to save on processing?
        var pk = record.Dynamodb.NewImage.PK();
        if (pk.StartsWith("DELETED#"))
        {
            return Task.CompletedTask;
        }

        var participant = new Participant();

        _participantMapper.Map(record.Dynamodb.NewImage, participant);

        // if patrticipant has both identifiers then this is a linked record
        // TODO: Add an IsLinkedAccount() method
        // to do more rigourous checks on the linked status.
        if (participant.ParticipantIdentifiers.Count == 2)
        {
            // check if the record is already in the database
            var existingParticipant = _dbContext
                .ParticipantIdentifiers
                .Where(x => participant.ParticipantIdentifiers.Select(y => y.Value).Contains(x.Value))
                .Select(x => x.Participant)
                .SingleOrDefault();

            if (existingParticipant != null)
            {
                // if the record is already in the database then update the record
                _participantMapper.Map(record.Dynamodb.NewImage, existingParticipant);
                return Task.CompletedTask;
            }
        }

        _dbContext.Participants.Add(participant);
        // TODO: returning Task.CompletedTask means this code is no doing anything asynchronously.
        // Ensure all db access is async, pass cancellation token down from the highest level.
        return Task.CompletedTask;
    }

    private Task ProcessModify(DynamoDBEvent.DynamodbStreamRecord record)
    {
        var pk = record.Dynamodb.NewImage.PK();
        var participant = _dbContext.Participants.Single(x => x.ParticipantIdentifiers.Any(y => y.Value == pk));

        _participantMapper.Map(record.Dynamodb.NewImage, participant);

        return Task.CompletedTask;
    }

    private Task ProcessRemove(DynamoDBEvent.DynamodbStreamRecord record)
    {
        var pk = record.Dynamodb.OldImage.PK();
        var participant = _dbContext.Participants.Single(x => x.ParticipantIdentifiers.Any(y => y.Value == pk));

        // TODO: handle soft-delete transparently
        participant.IsDeleted = true;
        participant.Email = null;
        participant.FirstName = null;
        participant.LastName = null;
        participant.MobileNumber = null;
        participant.LandlineNumber = null;
        participant.RegistrationConsent = false;
        participant.RemovalOfConsentRegistrationAtUtc = _clock.Now();
        participant.UpdatedAt = _clock.Now();
            participant.Disability = null;
        participant.Address.Clear();
        participant.HealthConditions.Clear();
        }

        return Task.CompletedTask;
    }
}
