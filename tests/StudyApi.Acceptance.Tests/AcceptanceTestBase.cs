using System;
using System.Net;
using System.Security.Claims;
using Application.Settings;
using Dte.Api.Acceptance.Test.Helpers.Clients;
using Dte.Api.Acceptance.Test.Helpers.Extensions;
using Dte.Common.Authentication;
using Dte.Common.Http;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using StudyApi.Acceptance.Tests.Clients;
using StudyApi.Common;

namespace StudyApi.Acceptance.Tests
{
    public abstract class AcceptanceTestBase
    {
        private ApiClient _apiClient;
        
        // Override this method to provide a custom claims
        protected virtual void CreateApiWebApplicationFactory()
        {
            TestApi = new ApiWebApplicationFactory();
        }
        
        [SetUp]
        public void Setup()
        {
            CreateApiWebApplicationFactory();
            Scope = TestApi.Services.CreateScope();

            IdentitySettings = Scope.ServiceProvider.GetService<IdentitySettings>();
            ClientsSettings = Scope.ServiceProvider.GetService<ClientsSettings>();
            
            var httpClient = TestApi.CreateClient();
            _apiClient = ApiClientFactory.For(httpClient, "TestClient");
            BaseAddress = httpClient.BaseAddress;
            
            StudyApiClient = new StudyApiClient(_apiClient);
        }
        
        protected ApiWebApplicationFactory TestApi;
        protected IServiceScope Scope { get; private set; }
        protected Uri BaseAddress { get; private set; }
        protected StudyApiClient StudyApiClient { get; private set; }
        protected IdentitySettings IdentitySettings { get; private set; }
        protected ClientsSettings ClientsSettings { get; private set; }

        protected void LoginAsAdmin()
        {
            TestApi.AddClaims(new Claim("cognito:username", $"{Guid.NewGuid().ToString()}"));
            TestApi.AddClaims(new Claim("cognito:groups", AppRoles.Admin));
        }
        
        protected void LoginAsParticipant()
        {
            TestApi.AddClaims(new Claim("cognito:username", $"{Guid.NewGuid().ToString()}"));
        }
        
        protected void Logout()
        {
            TestApi.RemoveClaim("cognito:username");
        }
        
        protected static void AssertResponseStatusCode(IStubApiClient client, HttpStatusCode statusCode)
        {
            client.LastResponse().ShouldHaveStatusCode(statusCode);
        }

        protected static void AssertResponseContentType(IStubApiClient client, string contentType)
        {
            client.LastResponse().ShouldHaveContentType(contentType);
        }

        [TearDown]
        public void Dispose()
        {
            Scope.Dispose();
            TestApi?.Dispose();
            _apiClient?.Dispose();
        }
    }
}