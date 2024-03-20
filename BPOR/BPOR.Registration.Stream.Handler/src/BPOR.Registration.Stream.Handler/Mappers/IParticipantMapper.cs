using Amazon.DynamoDBv2.Model;
using BPOR.Domain.Entities;

namespace BPOR.Registration.Stream.Handler.Mappers;

public interface IParticipantMapper
{
    List<Identifier> ExtractIdentifiers(Dictionary<string, AttributeValue> newImage);
    AuroraParticipant Map(Dictionary<string, AttributeValue> record, AuroraParticipant destination);
}
