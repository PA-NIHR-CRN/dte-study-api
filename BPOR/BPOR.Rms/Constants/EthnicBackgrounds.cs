using System;
namespace BPOR.Rms.Constants
{
    public class EthnicBackgrounds
    {
        public EthnicBackgrounds()
        {
        }

        private static List<Dictionary<string, string>> Asian = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string> { { "label", "Bangladeshi" }, { "value", "Bangladeshi" } },
            new Dictionary<string, string> { { "label", "Chinese" }, { "value", "Chinese" } },
            new Dictionary<string, string> { { "label", "Indian" }, { "value", "Indian" } },
            new Dictionary<string, string> { { "label", "Pakistani" }, { "value", "Pakistani" } },
            new Dictionary<string, string> { { "label", "Another Asian or Asian British background" }, { "value", "Another Asian or Asian British background" } },

        };

        private static List<Dictionary<string, string>> Black = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string> { { "label", "African" }, { "value", "African" } },
            new Dictionary<string, string> { { "label", "Black British" }, { "value", "Black British" } },
            new Dictionary<string, string> { { "label", "Caribbean" }, { "value", "Caribbean" } },
            new Dictionary<string, string> { { "label", "Another Black, African, Black British or Caribbean background" }, { "value", "Another Black, African, Black British or Caribbean background" } }
        };

        private static List<Dictionary<string, string>> Mixed = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string> { { "label", "Asian and White" }, { "value", "Asian and White" } },
            new Dictionary<string, string> { { "label", "Black African and White" }, { "value", "Black African and White" } },
            new Dictionary<string, string> { { "label", "Black Caribbean and White" }, { "value", "Black Caribbean and White" } },
            new Dictionary<string, string> { { "label", "Another mixed background" }, { "value", "Another mixed background" } }
        };
        private static List<Dictionary<string, string>> White = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string> { { "label", "British, English, Northern Irish, Scottish, or Welsh" }, { "value", "British, English, Northern Irish, Scottish, or Welsh" } },
            new Dictionary<string, string> { { "label", "Irish" }, { "value", "Irish" } },
            new Dictionary<string, string> { { "label", "Irish Traveller" }, { "value", "Irish Traveller" } },
            new Dictionary<string, string> { { "label", "Roma" }, { "value", "Roma" } },
            new Dictionary<string, string> { { "label", "Another White background" }, { "value", "Another White background" } }
        };
        private static List<Dictionary<string, string>> Other = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string> { { "label", "Arab" }, { "value", "Arab" } },
            new Dictionary<string, string> { { "label", "Any other ethnic group" }, { "value", "Any other ethnic group" } }
        };

        //return correct ethnic Background option for ehtnic group value
        public static List<Dictionary<string, string>> getEthnicBackground(string ethnicGroup)
        {
            switch (ethnicGroup)
            {
                case "Asian":
                    return Asian;
                case "Black":
                    return Black;
                case "Mixed":
                    return Mixed;
                case "White":
                    return White;
                case "Other":
                    return Other;
                default:
                    return null;
            }
            
        }

    }
}

