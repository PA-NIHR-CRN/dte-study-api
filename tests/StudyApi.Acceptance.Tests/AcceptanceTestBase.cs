using System;
using System.Net;
using System.Security.Claims;
using Dte.Api.Acceptance.Test.Helpers.Clients;
using Dte.Api.Acceptance.Test.Helpers.Extensions;
using Dte.Common.Authentication;
using Dte.Common.Http;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using StudyApi.Acceptance.Tests.Clients;

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
        protected ClientsSettings ClientsSettings { get; private set; }

        protected void LoginAsAdmin()
        {
            TestApi.AddClaims(new Claim("cognito:username", $"{Guid.NewGuid().ToString()}"));
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
