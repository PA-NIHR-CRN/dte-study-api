using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NIHR.Infrastructure.Models;

namespace NIHR.Infrastructure.Clients
{
    public class LocationApiClient : ILocationApiClient
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
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri($"api/address/postcode/{postcode}", UriKind.Relative),
                Method = HttpMethod.Get,
            };

            var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<PostcodeAddressModel>>(jsonResponse);
            }
            else
            {
                _logger.LogError(
                    "Failed to retrieve addresses for postcode {Postcode}. Response status: {ResponseStatusCode}",
                    postcode, response.StatusCode);
                return new List<PostcodeAddressModel>();
            }
        }
        
        public async Task<LatLngModel> GetLatLngByPostcodeAsync(string postcode,
            CancellationToken cancellationToken = default)
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri($"api/address/latlng/{postcode}", UriKind.Relative),
                Method = HttpMethod.Get,
            };

            var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LatLngModel>(jsonResponse);
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
