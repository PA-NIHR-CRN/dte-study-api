using BPOR.Domain.Enums;
using NIHR.NotificationService.Enums;

namespace BPOR.Rms.Models.Email;

public class EmailSuccessViewModel
{
    public int? StudyId { get; set; }
    public string? StudyName { get; set; }
    public NotificationContactMethod ContactMethod { get; set; }
}
