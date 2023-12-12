using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.DynamoDBEvents;

namespace DYNAMO.STREAM.INGESTOR.Services;

public class DynamoDbEventService : IDynamoDbEventService
{
    public DynamoDBEvent CreateParticipantInsertEvent(Dictionary<string, AttributeValue> participant)
    {
        var dynamoDbEvent = new DynamoDBEvent
        {
            Records = new List<DynamoDBEvent.DynamodbStreamRecord>
            {
                new ()
                {
                    EventID = Guid.NewGuid().ToString(),
                    EventName = OperationType.INSERT,
                    Dynamodb = new StreamRecord
                    {
                        ApproximateCreationDateTime = DateTime.UtcNow,
                        Keys = new Dictionary<string, AttributeValue>
                        {
                            { "PK", participant["PK"] },
                            { "SK", participant["SK"] }
                        },
                        NewImage = participant,
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
