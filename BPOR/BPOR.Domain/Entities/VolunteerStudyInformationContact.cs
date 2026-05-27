namespace BPOR.Domain.Entities;

public class  VolunteerStudyInformationContact
{
    public int Id { get; set; }
    public int VolunteerStudyInformationId { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Organisation { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; }
    
    public VolunteerStudyInformation VolunteerStudyInformation { get; set; }
}