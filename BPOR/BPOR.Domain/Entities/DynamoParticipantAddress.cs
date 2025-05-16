namespace BPOR.Domain.Entities;

public class DynamoParticipantAddress
{
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string AddressLine3 { get; set; }
    public string AddressLine4 { get; set; }
    public string Town { get; set; }
    public string? CanonicalTown { get; set; }
    public string Postcode { get; set; }

    private static string GetOutcodeFromPostcode(string postcode)
    {
        if (string.IsNullOrWhiteSpace(postcode)) return null;
        var postcodeWithoutSpace = postcode.Replace(" ", "");
        return postcodeWithoutSpace[..^3];
    }

    public DynamoParticipantAddress Clear()
    {
        AddressLine1 = null;
        AddressLine2 = null;
        AddressLine3 = null;
        AddressLine4 = null;
        Town = Town;
        CanonicalTown = CanonicalTown;
        Postcode = GetOutcodeFromPostcode(Postcode);
        return this;
    }
}
