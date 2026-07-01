namespace BPOR.Rms.Models.Study;

public class ActionLink
{
    public string Text { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public HyperlinkTarget Target { get; set; } = HyperlinkTarget.Self;
}