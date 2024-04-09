using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BPOR.Rms.Models.Email;

public class SetupCampaignViewModel
{
    public int MaxNumbers { get; set; }
    
    [DisplayName("How many volunteers do you want to send it to?")]
    public int TotalVolunteers { get; set; }
    public string StudyName { get; set; }
    
    [DisplayName("Select email template")]
    public string SelectedTemplate { get; set; }
    
    [DisplayName("Preview email")]
    public string PreviewEmails { get; set; }
}
