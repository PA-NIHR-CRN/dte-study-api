namespace BPOR.Rms.Ms4.Models;

public class StudyDetailsViewModel
{
    public string? StudyTitle { get; set; }
    public string? StudyDescription { get; set; }
    public bool? HasMoreThanOneResearchLocation { get; set; }
    public bool? HasOnePersonResponsibleForRecruiting { get; set; }
    public string? ChiefInvestigatorName { get; set; }
    public string? ChiefInvestigatorEmail { get; set; }
    public bool? IsChiefInvestigatorMainContact { get; set; }
    public string? MainContactName { get; set; }
    public string? MainContactEmail { get; set; }
    public string? MainContactRole { get; set; }
}