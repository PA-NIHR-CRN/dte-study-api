namespace BPOR.Domain.Entities;

public class VolunteerStudyInformationSite
{
    public int Id { get; set; }
    public int VolunteerStudyInformationId { get; set; }
    
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressLine4 { get; set; }
    public string? AddressLine5 { get; set; }
    public string Postcode { get; set; }
    
    public VolunteerStudyInformation VolunteerStudyInformation { get; set; }
}