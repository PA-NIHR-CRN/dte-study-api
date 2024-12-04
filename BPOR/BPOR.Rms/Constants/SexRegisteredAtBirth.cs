using System;
namespace BPOR.Rms.Constants
{
	public class SexRegisteredAtBirth
    {
		public SexRegisteredAtBirth()
		{
		}

        private static List<Dictionary<string, string>> SexRegisteredAtBirthOptions = new List<Dictionary<string, string>>
        {

            new Dictionary<string, string> { { "label", "Female" }, { "value", "2" } },
            new Dictionary<string, string> { { "label", "Male" }, { "value", "1" } }

        };

        public static List<Dictionary<string, string>> getSexRegisteredAtBirthOptions()
        {
            return SexRegisteredAtBirthOptions;
        }

    }
}

