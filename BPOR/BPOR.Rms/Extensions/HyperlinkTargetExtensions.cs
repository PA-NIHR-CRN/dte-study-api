using BPOR.Rms.Models.Study;

namespace BPOR.Rms.Extensions;

public static class HyperlinkTargetExtensions
{
    public static string ToAttributeValue(this HyperlinkTarget value) =>
        value switch
        {
            HyperlinkTarget.Top => "_top",
            HyperlinkTarget.Blank => "_blank",
            HyperlinkTarget.Parent => "_parent",
            HyperlinkTarget.Self => "_self",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
}