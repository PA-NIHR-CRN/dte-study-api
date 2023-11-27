using Amazon.DynamoDBv2;
using Amazon.Lambda.DynamoDBEvents;
using Dte.Common.Contracts;
using DYNAMO.STREAM.HANDLER.Contracts;
using DYNAMO.STREAM.HANDLER.Entities;
using DYNAMO.STREAM.HANDLER.Extensions;
using Microsoft.Extensions.Logging;
using Polly;

namespace DYNAMO.STREAM.HANDLER.Handlers;

public class StreamHandler : IStreamHandler
{
    private readonly ParticipantDbContext _dbContext;
    private readonly ILogger<StreamHandler> _logger;
    private readonly IParticipantMapper _participantMapper;
    private readonly IClock _clock;
    private readonly IAuroraRepository _auroraRepository;
    private readonly IAsyncPolicy _retryPolicy;

    public StreamHandler(ParticipantDbContext dbContext, IAsyncPolicy retryPolicy, ILogger<StreamHandler> logger,
        IParticipantMapper participantMapper, IClock clock, IAuroraRepository auroraRepository)
    {
        _dbContext = dbContext;
        _retryPolicy = retryPolicy;
        _logger = logger;
        _participantMapper = participantMapper;
        _clock = clock;
        _auroraRepository = auroraRepository;
    }

    public async Task ProcessStreamAsync(DynamoDBEvent dynamoDbEvent, CancellationToken cancellationToken)
    {
        foreach (var record in dynamoDbEvent.Records)
        {
            _logger.LogInformation("**** Processing {EventType} Event ID: {RecordEventId}", record.EventName,
                record.EventID);

            try
            {
                await _retryPolicy.ExecuteAsync(async () => await ProcessRecordAsync(record, cancellationToken));
                // TODO: should this be moved to the _auroraRepository to encapsulate the logic?  Doing so would mean that the _dbContext could be removed from the constructor.
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "**** Error processing Event ID: {RecordEventId}", record.EventID);
            }

            _logger.LogInformation("****  Finished processing Event ID: {RecordEventId}", record.EventID);
        }
    }

    private Task ProcessRecordAsync(DynamoDBEvent.DynamodbStreamRecord record, CancellationToken cancellationToken)
    {
        if (record.EventName == OperationType.INSERT)
        {
            return ProcessInsertAsync(record, cancellationToken);
        }
        else if (record.EventName == OperationType.MODIFY)
        {
            return ProcessModifyAsync(record, cancellationToken);
        }
        else if (record.EventName == OperationType.REMOVE)
        {
            return ProcessRemoveAsync(record, cancellationToken);
        }
        else
        {
            throw new NotSupportedException($"Unknown Event Name {record.EventName}");
        }
    }
    
    private async Task ProcessInsertAsync(DynamoDBEvent.DynamodbStreamRecord record, CancellationToken cancellationToken)
    {
        var pk = record.Dynamodb.NewImage.PK();
        if (pk.StartsWith("DELETED#"))
        {
            return;
        }

        var participant = new Participant();
        _participantMapper.Map(record.Dynamodb.NewImage, participant);
    
        var isLinkedAccount = await _auroraRepository.IsLinkedAccountAsync(participant, cancellationToken);
        if (isLinkedAccount)
        {
            var existingParticipant = await _auroraRepository.GetParticipantAsync(participant, cancellationToken);
            if (existingParticipant != null)
            {
                _participantMapper.Map(record.Dynamodb.NewImage, existingParticipant);
            }
        }
        else
        {
            // TODO: should this be added to _auroraRepository as it's more of a context thing?
            await _dbContext.Participants.AddAsync(participant, cancellationToken);
        }
    }

    private async Task ProcessModifyAsync(DynamoDBEvent.DynamodbStreamRecord record, CancellationToken cancellationToken)
    {
        var pk = record.Dynamodb.NewImage.PK();
        var participant = await _auroraRepository.GetParticipantByIdAsync(pk, cancellationToken);

        if (participant != null)
        {
            _participantMapper.Map(record.Dynamodb.NewImage, participant );
        }
        else
        {
            _logger.LogWarning("Participant with PK {PK} not found for modification", pk);
        }
    }

    private async Task ProcessRemoveAsync(DynamoDBEvent.DynamodbStreamRecord record, CancellationToken cancellationToken)
    {
        var pk = record.Dynamodb.OldImage.PK();
        var participant = await _auroraRepository.GetParticipantByIdAsync(pk, cancellationToken);

        if (participant != null)
        {
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
        else
        {
            _logger.LogWarning("Participant with PK {PK} not found for deletion.", pk);
        }
    }
}
