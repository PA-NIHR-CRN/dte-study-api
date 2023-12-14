using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.DynamoDBEvents;

namespace Dynamo.Stream.Ingestor.Services;

public class DynamoDbEventService : IDynamoDbEventService
{
    public DynamoDBEvent CreateEvent(OperationType eventType, Dictionary<string, AttributeValue> newImage = null!, Dictionary<string, AttributeValue> oldImage = null!)
    {
        var dynamoDbEvent = new DynamoDBEvent
        {
            Records = new List<DynamoDBEvent.DynamodbStreamRecord>
            {
                new ()
                {
                    EventID = Guid.NewGuid().ToString(),
                    EventName = eventType,
                    Dynamodb = new StreamRecord
                    {
                        ApproximateCreationDateTime = DateTime.UtcNow,
                        Keys = new Dictionary<string, AttributeValue>
                        {
                            { "PK", newImage["PK"] },
                            { "SK", newImage["SK"] }
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
}
