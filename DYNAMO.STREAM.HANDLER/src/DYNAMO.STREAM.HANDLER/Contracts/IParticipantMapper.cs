using Amazon.DynamoDBv2.Model;
using DYNAMO.STREAM.HANDLER.Entities;

namespace DYNAMO.STREAM.HANDLER.Contracts;

public interface IParticipantMapper
{
    void Map (Dictionary<string,AttributeValue> record, Participant destination);
}
