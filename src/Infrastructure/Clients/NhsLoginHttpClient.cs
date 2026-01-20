using Application.Contracts;
using Application.Responses.V1.Users;
using Application.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        public async Task<TokenResponse> GetTokensFromAuthorizationCode(
            string authorizationCode,
            string redirectUrl,
            string nonce,
            CancellationToken cancellationToken = default)
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

            var tokens = await response.Content.ReadFromJsonAsync<TokenResponse>(
                cancellationToken: cancellationToken
            );

            if (tokens == null || string.IsNullOrWhiteSpace(tokens.IdToken))
            {
                throw new SecurityTokenException("ID token missing from token response");
            }

            ValidateNonce(tokens.IdToken, nonce);

            return tokens;
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

        private static void ValidateNonce(string idToken, string expectedNonce)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(idToken);

            if (!jwt.Payload.TryGetValue("nonce", out var nonceObj))
                throw new SecurityTokenException("Nonce missing from id_token");

            var actualNonce = nonceObj?.ToString();

            if (!FixedTimeEquals(actualNonce, expectedNonce))
                throw new SecurityTokenException("Invalid nonce");
        }

        private static bool FixedTimeEquals(string? a, string? b)
        {
            if (a == null || b == null || a.Length != b.Length)
                return false;

            var result = 0;
            for (int i = 0; i < a.Length; i++)
                result |= a[i] ^ b[i];

            return result == 0;
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