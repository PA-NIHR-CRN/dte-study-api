using System;
using System.Collections.Generic;
using System.Security.Claims;
using Application.Contracts;
using Application.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParticipantApi.Acceptance.Tests.Stubs;

namespace StudyApi.Acceptance.Tests
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private readonly List<Claim> _claims;
        public IConfiguration Configuration { get; private set; }

        public ApiWebApplicationFactory()
        {
            _claims = new List<Claim>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                // Override config
                var configurationBuilder = new ConfigurationBuilder();
                config.Sources.Clear();
                
                Configuration = configurationBuilder
                    .AddJsonFile("appsettings.json")
                    .Build();
                config.AddConfiguration(Configuration);
            });

            // Shared test setup
            builder.ConfigureTestServices(services =>
            {
                // Configuration
                var awsSettings = Configuration.GetSection(AwsSettings.SectionName).Get<AwsSettings>();
                if (awsSettings == null) throw new Exception("Could not bind the aws settings, please check configuration");
                var cpmsSettings = Configuration.GetSection(CpmsSettings.SectionName).Get<CpmsSettings>();
                if (cpmsSettings == null) throw new Exception("Could not bind the cpms settings, please check configuration");
                var identitySettings = Configuration.GetSection(IdentitySettings.SectionName).Get<IdentitySettings>();
                if (identitySettings == null) throw new Exception("Could not bind the identity settings, please check configuration");
                var emailSettings = Configuration.GetSection(EmailSettings.SectionName).Get<EmailSettings>();
                if (emailSettings == null) throw new Exception("Could not bind the email settings, please check configuration");

                services.AddSingleton(awsSettings);
                services.AddSingleton(cpmsSettings);
                services.AddSingleton(identitySettings);
                services.AddSingleton(emailSettings);
                
                services.AddAuthentication("Test").AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                
                services.AddAuthorization(options =>
                {
                    options.AddPolicy("AnyAuthenticatedUser", policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.AuthenticationSchemes.Add("Test");
                    });
                    
                    options.AddPolicy("Admin", policy => policy
                        .RequireAuthenticatedUser()
                        .AddAuthenticationSchemes("Test")
                    );
                });
                
                services.AddScoped(_ => new MockAuthUser(_claims));
                
                services.AddSingleton<IParticipantRepository, ParticipantRepositoryStub>();
            });
        }

        public void AddClaims(params Claim[] claims)
        {
            foreach (var claim in claims)
            {
                _claims.RemoveAll(x => x.Type == claim.Type);
                _claims.Add(claim);
            }
        }

        public void RemoveClaim(string claimType)
        {
            _claims.RemoveAll(x => x.Type == claimType);
        }

        public void ClearClaims()
        {
            foreach (var claim in _claims)
            {
                _claims.Remove(claim);
            }
        }
    }

}