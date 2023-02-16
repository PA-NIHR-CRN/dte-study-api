using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Responses.V1.Cpms;
using Application.Settings;
using Dte.Common.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Infrastructure.Clients
{
    public class CpmsHttpClient : ICpmsHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CpmsHttpClient> _logger;

        public CpmsHttpClient(HttpClient httpClient, ILogger<CpmsHttpClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<CpmsApiResponseRoot> GetStudyAsync(long id)
        {
            var requestUri = $"api/v3/Study/{id}";
            _logger.LogInformation($"Calling http: {_httpClient.BaseAddress}{requestUri}");
            var response = await _httpClient.GetAsync(requestUri);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("CpmsStudy", id);
            }

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error calling CPMS API - response StatusCode: {response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
            }

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<CpmsApiResponseRoot>(responseString, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
    
    public class CpmsHttpMessageHandler : DelegatingHandler
    {
        private readonly CpmsSettings _cpmsSettings;

        public CpmsHttpMessageHandler(CpmsSettings cpmsSettings)
        {
            _cpmsSettings = cpmsSettings;
        }
        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("UserName", _cpmsSettings.UserName);
            request.Headers.Add("Password", _cpmsSettings.Password);
            
            return base.SendAsync(request, cancellationToken);
        }
    }
}