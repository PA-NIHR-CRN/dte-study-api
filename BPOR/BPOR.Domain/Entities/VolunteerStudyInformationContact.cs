namespace BPOR.Domain.Entities;

public class  VolunteerStudyInformationContact
{
    public long Id { get; set; }
    public long VolunteerStudyInformationId { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Organisation { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; }
    
    public VolunteerStudyInformation VolunteerStudyInformation { get; set; }
}

public class VolunteerStudyInformationSite
{
    public long Id { get; set; }
    public long VolunteerStudyInformationId { get; set; }
    
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressLine4 { get; set; }
    public string? AddressLine5 { get; set; }
    public string Postcode { get; set; }
    
    public VolunteerStudyInformation VolunteerStudyInformation { get; set; }
}