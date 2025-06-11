using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDBupdate
{
    static class OrdnanceSurveyCountries
    {
        private static string c = string.Empty;
        public static string CodeQueryParams
        {
            get
            {
                if (c == string.Empty)
                {
                    c = BuildCountryCodeQueryParams(Countries);
                }
                return c;
            }
        }

        private static IEnumerable<CountryModel> Countries { get; } = new List<CountryModel>
        {
            new CountryModel("ENGLAND", "e"),
            new CountryModel("SCOTLAND", "s"),
            new CountryModel("WALES", "w"),
            new CountryModel("NORTHERN IRELAND", "n"),
        };

        private static string BuildCountryCodeQueryParams(IEnumerable<CountryModel> countryModels)
        {
            var sb = new StringBuilder("fq=");

            foreach (var country in countryModels)
            {
                sb.Append($"COUNTRY_CODE:{country.CountryCode} ");
            }

            return sb.ToString();
        }

        public class CountryModel
        {
            public CountryModel(string countryName, string countryCode)
            {
                CountryName = countryName;
                CountryCode = countryCode;
            }

            public string CountryName { get; set; }
            public string CountryCode { get; set; }
        }
    }
}
