namespace BPOR.Rms.Constants;

public class ResearcherEmails
{
    private static List<Dictionary<string, string>> ResearcherEmailOptions = new List<Dictionary<string, string>>
    {

        new Dictionary<string, string> { { "label", "Send introductory email" }, { "value", "1" } },
        new Dictionary<string, string> { { "label", "Next steps- offer pre-screener" }, { "value", "2" } },
        new Dictionary<string, string> { { "label", "Next steps without pre-screener" }, { "value", "3" } }

    };

    public static List<Dictionary<string, string>> GetResearcherEmailOptions()
    {
        return ResearcherEmailOptions;
    }
}