using BPOR.Domain.Enums;

namespace BPOR.Rms.Models.Email;

public class EmailSuccessViewModel
{
    public int? StudyId { get; set; }
    public string? StudyName { get; set; }
    public ContactMethods ContactMethod { get; set; }
}
