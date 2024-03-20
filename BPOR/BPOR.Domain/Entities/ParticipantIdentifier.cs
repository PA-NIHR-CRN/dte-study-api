using System.ComponentModel.DataAnnotations;
using BPOR.Domain.Entities.RefData;
using NIHR.Infrastructure.Entities;

namespace BPOR.Domain.Entities;

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
    public AuroraParticipant Participant { get; set; }
}
