using System.Collections.Generic;
using System.Linq;
using Application.Responses.V1.ReferenceData;

namespace Application.Mappings.ReferenceData
{
    public static class DemographicsMapper
    {
        public static Dictionary<string, EthnicityResponse> MapTo(Dictionary<string, Dte.Reference.Data.Api.Client.Responses.EthnicityResponse> source)
        {
            return source.ToDictionary(x => x.Key, x => new EthnicityResponse
            {
                ShortName = x.Value.ShortName,
                LongName = x.Value.LongName,
                Description = x.Value.Description,
                Backgrounds = x.Value.Backgrounds
            });
        }
    }
}