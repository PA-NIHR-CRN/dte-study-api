namespace BPOR.Rms.Models.Email;

public class EmailSuccessViewModel
{
    public int? StudyId { get; set; }
    public string? StudyName { get; set; }
    public string? ContactPreference { get; set; } = "Email";
}
