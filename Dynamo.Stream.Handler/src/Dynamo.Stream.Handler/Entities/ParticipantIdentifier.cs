using Dynamo.Stream.Handler.Entities.RefData;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Dynamo.Stream.Handler.Entities;


public class ParticipantIdentifier : ISoftDelete
{
    public ParticipantIdentifier()
    {
        Type = null!;
        Participant = null!;
    }

    public int Id { get; set; }
    public int ParticipantId { get; set; }

    [Required]
    public Guid Value { get; set; }

    public int IdentifierTypeId { get; set; }
    public bool IsDeleted { get; set; }

    public IdentifierType Type { get; set; }
    public Participant Participant { get; set; }
}
