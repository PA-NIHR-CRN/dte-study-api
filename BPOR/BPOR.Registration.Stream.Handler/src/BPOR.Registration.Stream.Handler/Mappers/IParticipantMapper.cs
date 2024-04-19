using Amazon.DynamoDBv2.Model;
using BPOR.Domain.Entities;

namespace BPOR.Registration.Stream.Handler.Mappers;

public interface IParticipantMapper
{
    List<Identifier> ExtractIdentifiers(Dictionary<string, AttributeValue> newImage);
    Task<Participant> Map(Dictionary<string, AttributeValue> record, Participant destination, CancellationToken cancellationToken);
}
