namespace BPOR.Rms.Models.Study;

public class SendIntroductoryEmailViewModel
{
    public int Id { get; set; }
    public string StudyName { get; set; }
    public bool? IncludePreScreener { get; set; }   
}