using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NIHR.Infrastructure;
using NIHR.Infrastructure.Models;

namespace BPOR.Infrastructure.Clients
{
    public class LocationApiClient : IPostcodeMapper
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LocationApiClient> _logger;

        public LocationApiClient(HttpClient httpClient, ILogger<LocationApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<PostcodeAddressModel>> GetAddressesByPostcodeAsync(string postcode,
    CancellationToken cancellationToken)
        {
            try
            {
                var httpRequest = new HttpRequestMessage
                {
                    RequestUri = new Uri($"api/address/postcode/{postcode}", UriKind.Relative),
                    Method = HttpMethod.Get,
                };

                var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
                    return JsonConvert.DeserializeObject<IEnumerable<PostcodeAddressModel>>(jsonResponse)
                           ?? Enumerable.Empty<PostcodeAddressModel>();
                }
                else
                {
                    _logger.LogError(
                        "Failed to retrieve addresses for postcode {Postcode}. Response status: {ResponseStatusCode}",
                        postcode, response.StatusCode);
                    return Enumerable.Empty<PostcodeAddressModel>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while retrieving addresses for postcode {Postcode}", postcode);
                return Enumerable.Empty<PostcodeAddressModel>();
            }
        }


        public async Task<CoordinatesModel> GetCoordinatesFromPostcodeAsync(string postcode,
            CancellationToken cancellationToken = default)
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri($"api/address/coordinates/{postcode}", UriKind.Relative),
                Method = HttpMethod.Get,
            };

            var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
                
                return JsonConvert.DeserializeObject<CoordinatesModel>(jsonResponse);
            }
            else
            {
                _logger.LogError(
                    "Failed to retrieve latlng for postcode {Postcode}. Response status: {ResponseStatusCode}",
                    postcode, response.StatusCode);
                return null;
            }
        }
    }
}
