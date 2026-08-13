namespace BPOR.Rms.VolunteerInformation.Models;

public class VsiSiteModel
{
    public int Id { get; set; }
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressLine4 { get; set; }
    public string? AddressLine5 { get; set; }
    public string Postcode { get; set; }

    public IEnumerable<string?> AllLines =>
        [AddressLine1, AddressLine2, AddressLine3, AddressLine4, AddressLine5, Postcode];
    
    public IEnumerable<string> AllSignificantLines =>
        AllLines.Where(i => !string.IsNullOrWhiteSpace(i))!;
}