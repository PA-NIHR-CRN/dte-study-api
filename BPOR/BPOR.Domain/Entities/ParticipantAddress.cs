using System.ComponentModel.DataAnnotations;
using NIHR.Infrastructure.Entities;

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
    public string? Postcode { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public int ParticipantId { get; set; }

    [Required]
    public Participant Participant { get; set; }
    public bool IsDeleted { get; set; }

    private static string? GetOutcodeFromPostcode(string? postcode)
    {
        if (string.IsNullOrWhiteSpace(postcode))
        {
            return null;
        }

        var postcodeWithoutSpace = postcode.Replace(" ", "");
        return postcodeWithoutSpace[..^3];
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
