using System.ComponentModel.DataAnnotations;
using NIHR.Infrastructure.EntityFrameworkCore;

namespace BPOR.Domain.Entities;

public class ParticipantAddress : IPersonalInformation, ISoftDelete
{
    public ParticipantAddress()
    {
        Participant = null!;
    }

    [Key]
    public int Id { get; set; }

    [MaxLength(255)]
    public string? AddressLine1 { get; set; }
    [MaxLength(255)]
    public string? AddressLine2 { get; set; }
    [MaxLength(255)]
    public string? AddressLine3 { get; set; }
    [MaxLength(255)]
    public string? AddressLine4 { get; set; }
    [MaxLength(255)]
    public string? Town { get; set; }
    [MaxLength(255)]
    public string? CanonicalTown { get; set; }
    [MaxLength(255)]
    public string? Postcode { get; set; }

    public int ParticipantId { get; set; }

    public Participant Participant { get; set; }
    public bool IsDeleted { get; set; }

    private static string? GetOutcodeFromPostcode(string? postcode)
    {
        if (Rbec.Postcodes.Postcode.TryParse(postcode, out var validPostcode))
        {
            return validPostcode.ToString().Split(' ').First();
        }
        else
        {
            return null;
        }
    }
    public void Anonymise()
    {
        AddressLine1 = null;
        AddressLine2 = null;
        AddressLine3 = null;
        AddressLine4 = null;
        Town = null;
        Postcode = GetOutcodeFromPostcode(Postcode);
    }
}
