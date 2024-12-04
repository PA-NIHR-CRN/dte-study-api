using System;
namespace BPOR.Rms.Constants
{
	public class EthnicGroups
	{
		public EthnicGroups()
		{
		}

        private static List<Dictionary<string, string>> EthnicGroupOptions = new List<Dictionary<string, string>>
        {

            new Dictionary<string, string> { { "label", "Asian or Asian British" }, { "value", "Asian" } },
            new Dictionary<string, string> { { "label", "Black, African, Black British or Caribbean" }, { "value", "Black" } },
            new Dictionary<string, string> { { "label", "Mixed or multiple ethnic groups" }, { "value", "Mixed" } },
            new Dictionary<string, string> { { "label", "White" }, { "value", "White" } },
            new Dictionary<string, string> { { "label", "Other ethnic group" }, { "value", "Other" } }

        };

        public static List<Dictionary<string, string>> getEthnicGroupOptions()
        {
            return EthnicGroupOptions;
        }

    }
}

