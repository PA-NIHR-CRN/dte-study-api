using Amazon.DynamoDBv2.Model;
using DYNAMO.STREAM.HANDLER.Entities;

namespace DYNAMO.STREAM.HANDLER.Mappers;

public interface IParticipantMapper
{
    IList<(int type, string value)> ExtractIdentifiers(Dictionary<string, AttributeValue> newImage);
    Participant Map(Dictionary<string, AttributeValue> record, Participant destination);
}
