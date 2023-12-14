using Dynamo.Stream.Handler.Entities.RefData;
using System.ComponentModel.DataAnnotations;

namespace Dynamo.Stream.Handler.Entities;

public class ParticipantIdentifier : ISoftDelete
{
    public ParticipantIdentifier()
    {
        Value = null!;
        Type = null!;
        Participant = null!;
    }

    public int Id { get; set; }
    public int ParticipantId { get; set; }

    [Required]
    public string Value { get; set; }

    public int IdentifierTypeId { get; set; }
    public bool IsDeleted { get; set; }

    public IdentifierType Type { get; set; }
    public Participant Participant { get; set; }
}
