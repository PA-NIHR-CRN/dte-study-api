namespace BPOR.Rms.Constants;

public class ResearcherEmails
{
    public ResearcherEmails()
    {
    }
    
    private static List<Dictionary<string, string>> ResearcherEmailOptions = new List<Dictionary<string, string>>
    {

        new Dictionary<string, string> { { "label", "Send introductory email" }, { "value", "Introductory" } },
        new Dictionary<string, string> { { "label", "Next steps, offer pre-screener" }, { "value", "Offer-Pre-Screener" } },
        new Dictionary<string, string> { { "label", "Next steps, without pre-screener" }, { "value", "Without-Pre-Screener" } }

    };

    public static List<Dictionary<string, string>> getResearcherEmailOptions()
    {
        return ResearcherEmailOptions;
    }
}