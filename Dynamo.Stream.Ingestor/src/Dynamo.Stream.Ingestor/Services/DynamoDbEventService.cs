using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.DynamoDBEvents;
using Dynamo.Stream.Handler.Extensions;

namespace Dynamo.Stream.Ingestor.Services;

public class DynamoDbEventService : IDynamoDbEventService
{
    public DynamoDBEvent CreateEvent(OperationType eventType, Dictionary<string, AttributeValue> newImage = null!,
        Dictionary<string, AttributeValue> oldImage = null!)
    {
        var image = newImage ?? oldImage;
        if (image == null)
        {
            throw new ArgumentException("Either newImage or oldImage must be provided");
        }

        var dynamoDbEvent = new DynamoDBEvent
        {
            Records = new List<DynamoDBEvent.DynamodbStreamRecord>
            {
                new()
                {
                    EventID = Guid.NewGuid().ToString(),
                    EventName = eventType,
                    Dynamodb = new StreamRecord
                    {
                        ApproximateCreationDateTime = DateTime.UtcNow,
                        Keys = new Dictionary<string, AttributeValue>
                        {
                            { "PK", image["PK"] },
                            { "SK", image["SK"] }
                        },
                        NewImage = newImage ?? new Dictionary<string, AttributeValue>(),
                        OldImage = oldImage ?? new Dictionary<string, AttributeValue>(),
                        SequenceNumber = Guid.NewGuid().ToString(),
                        SizeBytes = 0,
                        StreamViewType = StreamViewType.NEW_AND_OLD_IMAGES,
                    },
                    EventSource = "aws:dynamodb",
                    EventVersion = "1.1",
                    AwsRegion = "eu-west-2",
                }
            }
        };

        return dynamoDbEvent;
    }

    public DynamoDBEvent CreateBatchEvent(IEnumerable<Dictionary<string, AttributeValue>> participants)
    {
        var records = new List<DynamoDBEvent.DynamodbStreamRecord>();

        foreach (var participant in participants)
        {
            var pk = participant.PK();
            var eventType = pk.StartsWith("DELETED#") ? OperationType.REMOVE : OperationType.INSERT;
            var dynamoDbRecord = new DynamoDBEvent.DynamodbStreamRecord
            {
                EventID = Guid.NewGuid().ToString(),
                EventName = eventType,
                Dynamodb = new StreamRecord
                {
                    ApproximateCreationDateTime = DateTime.UtcNow,
                    Keys = new Dictionary<string, AttributeValue>
                    {
                        { "PK", participant["PK"] },
                        { "SK", participant["SK"] }
                    },
                    NewImage = eventType == OperationType.INSERT
                        ? participant
                        : new Dictionary<string, AttributeValue>(),
                    OldImage = eventType == OperationType.REMOVE
                        ? participant
                        : new Dictionary<string, AttributeValue>(),
                    SequenceNumber = Guid.NewGuid().ToString(),
                    SizeBytes = 0, // Update as needed
                    StreamViewType = StreamViewType.NEW_AND_OLD_IMAGES,
                },
                EventSource = "aws:dynamodb",
                EventVersion = "1.1",
                AwsRegion = "eu-west-2",
            };

            records.Add(dynamoDbRecord);
        }

        return new DynamoDBEvent { Records = records };
    }
}
