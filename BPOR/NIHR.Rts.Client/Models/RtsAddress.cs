using System.Text.Json.Serialization;

namespace NIHR.Rts.Client;

public class RtsAddress
{
    public RtsAddress()
    {
    }

    public RtsAddress(string identifier, string addressLine1, string? addressLine2, string? addressLine3, string? addressLine4, string? addressLine5, string postcode)
    {
        Id = identifier;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        AddressLine3 = addressLine3;
        AddressLine4 = addressLine4;
        AddressLine5 = addressLine5;
        Postcode = postcode;
    }

    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressLine4 { get; set; }
    public string? AddressLine5 { get; set; }
    public string Postcode { get; set; }
    [JsonPropertyName("Identifier")]
    public string Id { get; set; }

    public override string ToString()
    {
        return string.Join(", ", ((string[])[AddressLine1, AddressLine2, AddressLine3, AddressLine4, AddressLine5, Postcode]).Where(x => !string.IsNullOrEmpty(x)));
    }
}