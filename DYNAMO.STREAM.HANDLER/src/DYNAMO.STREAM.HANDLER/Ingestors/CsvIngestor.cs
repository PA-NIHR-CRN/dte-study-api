using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.DynamoDBEvents;
using DYNAMO.STREAM.HANDLER.Contracts;
using Amazon.DynamoDBv2;

namespace DYNAMO.STREAM.HANDLER.Ingestors;

public class CsvIngestor : IDataIngestor
{
    private readonly ICsvService _csvService;
    private readonly IStreamHandler _streamHandler;

    public CsvIngestor(ICsvService csvService, IStreamHandler streamHandler)
    {
        _csvService = csvService;
        _streamHandler = streamHandler;
    }

    public async Task IngestDataAsync()
    {
        var dynamoDbEvent = new DynamoDBEvent
        {
            Records = new List<DynamoDBEvent.DynamodbStreamRecord>()
        };

        await foreach (var record in _csvService.ReadCsvAsync())
        {
            dynamoDbEvent.Records.Add(new DynamoDBEvent.DynamodbStreamRecord
            {
                EventID = Guid.NewGuid().ToString(),
                EventName = "INSERT",
                Dynamodb = new StreamRecord
                {
                    Keys = new Dictionary<string, AttributeValue>
                    {
                        { "Id", new AttributeValue { S = record.Id.ToString() } }
                    },
                    NewImage = new Dictionary<string, AttributeValue>
                    {
                        { "Id", new AttributeValue { S = record.Id.ToString() } },
                        { "FirstName", new AttributeValue { S = record.FirstName } },
                        { "LastName", new AttributeValue { S = record.LastName } },
                        { "Email", new AttributeValue { S = record.Email } },
                    },
                    StreamViewType = StreamViewType.NEW_AND_OLD_IMAGES
                }
            });
        }

        // Process the stream records after the loop
        await _streamHandler.ProcessStream(dynamoDbEvent);
    }
}
