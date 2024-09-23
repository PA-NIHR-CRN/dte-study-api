using Rbec.Postcodes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BPOR.Rms.Models.Volunteer
{
    public class AddressDetails
    {
        [JsonPropertyName("fullAddress")]
        public string FullAddress { get; set; }

        [JsonPropertyName("postcode")]
        public string? PostCode { get; set; }

        [JsonPropertyName("addressLine1")]
        public string? AddressLine1 { get; set; }

        [JsonPropertyName("addressLine2")]
        public string? AddressLine2 { get; set; }

        [JsonPropertyName("addressLine3")]
        public string? AddressLine3 { get; set; }

        [JsonPropertyName("addressLine4")]
        public string? AddressLine4 { get; set; }

        [JsonPropertyName("town")]
        public string? Town { get; set; }
    }
}
