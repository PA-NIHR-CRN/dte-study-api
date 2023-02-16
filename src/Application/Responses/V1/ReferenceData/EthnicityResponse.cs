using System.Collections.Generic;

namespace Application.Responses.V1.ReferenceData
{
    public class EthnicityResponse
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public List<string> Backgrounds { get; set; }
    }
}