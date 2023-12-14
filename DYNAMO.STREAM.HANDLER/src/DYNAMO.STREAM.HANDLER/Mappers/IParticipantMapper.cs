using Amazon.DynamoDBv2.Model;
using Dynamo.Stream.Handler.Entities;

namespace Dynamo.Stream.Handler.Mappers;

public interface IParticipantMapper
{
    List<Identifier> ExtractIdentifiers(Dictionary<string, AttributeValue> newImage);
    Participant Map(Dictionary<string, AttributeValue> record, Participant destination);
}
