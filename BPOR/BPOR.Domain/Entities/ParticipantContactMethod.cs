using System.ComponentModel.DataAnnotations;
using BPOR.Domain.Entities.RefData;
using BPOR.Domain.Enums;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class ParticipantContactMethod : ISoftDelete
{
    public ParticipantContactMethod()
    {
        Participant = null!;
        ContactMethod = null!;

    }

    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public int ContactMethodId { get; set; }
    
    public Participant Participant { get; set; }
    public ContactMethod ContactMethod { get; set; }
    public bool IsDeleted { get; set; }

}
