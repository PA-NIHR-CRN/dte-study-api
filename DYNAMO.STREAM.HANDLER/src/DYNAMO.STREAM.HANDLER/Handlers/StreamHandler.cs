using Amazon.Lambda.DynamoDBEvents;
using DYNAMO.STREAM.HANDLER.Contracts;
using DYNAMO.STREAM.HANDLER.Entities;
using DYNAMO.STREAM.HANDLER.Mappers;
using Microsoft.Extensions.Logging;
using Polly;

namespace DYNAMO.STREAM.HANDLER.Handlers;

public class StreamHandler : IStreamHandler
{
    private readonly ParticipantDbContext _dbContext;
    private readonly ILogger<StreamHandler> _logger;
    private readonly IAsyncPolicy _retryPolicy;

    public StreamHandler(ParticipantDbContext dbContext, IAsyncPolicy retryPolicy, ILogger<StreamHandler> logger)
    {
        _dbContext = dbContext;
        _retryPolicy = retryPolicy;
        _logger = logger;
    }

    public async Task ProcessStream(DynamoDBEvent dynamoDbEvent)
    {
        foreach (var record in dynamoDbEvent.Records)
        {
            _logger.LogInformation("**** Processing Event ID: {RecordEventId}", record.EventID);

            try
            {
                await _retryPolicy.ExecuteAsync(async () => await ProcessRecord(record));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "**** Error processing Event ID: {RecordEventId}", record.EventID);
            }

            _logger.LogInformation("****  Finished processing Event ID: {RecordEventId}", record.EventID);
        }

        await _dbContext.SaveChangesAsync();
    }

    private Task ProcessRecord(DynamoDBEvent.DynamodbStreamRecord record)
    { 
        // switch statement to handle different types of events
        switch (record.EventName)
        {
            case "INSERT":
                return ProcessInsert(record);
            case "MODIFY":
                return ProcessModify(record);
            case "REMOVE":
                return ProcessRemove(record);
            default:
                return Task.CompletedTask;
        }
        
    }
    
    private Task ProcessInsert(DynamoDBEvent.DynamodbStreamRecord record)
    {
        _logger.LogInformation("**** Processing INSERT Event ID: {RecordEventId}", record.EventID);
        _dbContext.Participants.Add(ParticipantMapper.Map(record));
        _retryPolicy.ExecuteAsync(async () => await _dbContext.SaveChangesAsync());
        return Task.CompletedTask;
    }
    
    private Task ProcessModify(DynamoDBEvent.DynamodbStreamRecord record)
    {
        // do we do anything different here?  Or can we just use one method?
        _logger.LogInformation("**** Processing MODIFY Event ID: {RecordEventId}", record.EventID);
        _dbContext.Participants.Update(ParticipantMapper.Map(record));
        _retryPolicy.ExecuteAsync(async () => await _dbContext.SaveChangesAsync());
        return Task.CompletedTask;
    }

    private Task ProcessRemove(DynamoDBEvent.DynamodbStreamRecord record)
    {
        _logger.LogInformation("**** Processing REMOVE Event ID: {RecordEventId}", record.EventID);
        // anonymise the record
        
        return Task.CompletedTask;
    }
}
