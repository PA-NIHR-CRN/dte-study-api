using Application.Contracts;
using Application.Responses.V1.Users;
using Application.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Clients
{
    public class NhsLoginHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NhsLoginHttpClient> _logger;
        private readonly IClientAssertionJwtProvider clientAssertionJwtProvider;
        private readonly NhsLoginSettings nhsLoginSettings;

        public NhsLoginHttpClient(HttpClient httpClient, ILogger<NhsLoginHttpClient> logger, IOptions<NhsLoginSettings> nhsLoginSettings, IClientAssertionJwtProvider clientAssertionJwtProvider)
        {
            _httpClient = httpClient;
            _logger = logger;
            this.clientAssertionJwtProvider = clientAssertionJwtProvider;
            this.nhsLoginSettings = nhsLoginSettings.Value;
        }

        public async Task<TokenResponse> GetTokensFromAuthorizationCode(string authorizationCode, string redirectUrl, CancellationToken cancellationToken = default)
        {
            var bearerToken = await clientAssertionJwtProvider.CreateClientAssertionJwtAsync(cancellationToken);

            var response = await _httpClient.PostAsync(nhsLoginSettings.TokenEndpoint,
                            new FormUrlEncodedContent(new Dictionary<string, string>
                            {
                                { "grant_type", "authorization_code" },
                                { "code", authorizationCode },
                                { "redirect_uri", redirectUrl },
                                { "client_id", nhsLoginSettings.ClientId},
                                { "client_assertion_type", "urn:ietf:params:oauth:client-assertion-type:jwt-bearer" },
                                { "client_assertion", bearerToken }
                            }), cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken: cancellationToken);
        }

        public async Task<NhsUserInfo> GetUserInfoAsync(string accessToken, CancellationToken cancellationToken = default)
        {
            var userInfoRequest = new HttpRequestMessage(HttpMethod.Get, nhsLoginSettings.UserInfoEndpoint);

            userInfoRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // get the user details from the NHS login API using the access token we got back from the token endpoint above
            var userInfo = await _httpClient.SendAsync(userInfoRequest, cancellationToken);

            if (!userInfo.IsSuccessStatusCode)
            {
                _logger.LogError("GetUserInfoAsync() - {Reason}, {Message}", userInfo.ReasonPhrase, await userInfo.Content.ReadAsStringAsync(cancellationToken));
            }

            userInfo.EnsureSuccessStatusCode();

            return await userInfo.Content.ReadFromJsonAsync<NhsUserInfo>(cancellationToken: cancellationToken);
        }
    }

    public class NhsUserInfo
    {
        [JsonPropertyName("given_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("family_name")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("birthdate")]
        public DateTime? DateOfBirth { get; set; }

        [JsonPropertyName("sub")]
        public string NhsId { get; set; }

        [JsonPropertyName("nhs_number")]
        public string NhsNumber { get; set; }
    }
}