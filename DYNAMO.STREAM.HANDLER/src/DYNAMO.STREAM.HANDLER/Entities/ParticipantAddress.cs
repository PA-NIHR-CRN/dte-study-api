using System.ComponentModel.DataAnnotations;

namespace DYNAMO.STREAM.HANDLER.Entities;

public class ParticipantAddress
{
    public ParticipantAddress()
    {
        Participant = null!;
    }

    [Key] 
    public int Id { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressLine4 { get; set; }
    public string? Town { get; set; }
    public string? Postcode { get; set; }
    public int ParticipantId { get; set; }

    [Required]
    public Participant Participant { get; set; }
    
    private static string? GetOutcodeFromPostcode(string? postcode)
    {
        if (string.IsNullOrWhiteSpace(postcode))
        {
            return null;
        }

        var postcodeWithoutSpace = postcode.Replace(" ", "");
        return postcodeWithoutSpace[..^3];
    }

    public void Clear()
    {
        AddressLine1 = null;
        AddressLine2 = null;
        AddressLine3 = null;
        AddressLine4 = null;
        Town = null;
        Postcode = GetOutcodeFromPostcode(Postcode);
    }
}
