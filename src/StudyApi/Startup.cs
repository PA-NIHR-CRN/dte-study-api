using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using Application.Settings;
using Dte.Common.Authentication;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using StudyApi.Behaviours;
using StudyApi.Common;
using StudyApi.DependencyRegistrations;
using StudyApi.Extensions;
using StudyApi.HealthChecks;

namespace StudyApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuration
            var awsSettings = Configuration.GetSection(AwsSettings.SectionName).Get<AwsSettings>();
            if (awsSettings == null) throw new Exception("Could not bind the aws settings, please check configuration");
            var cpmsSettings = Configuration.GetSection(CpmsSettings.SectionName).Get<CpmsSettings>();
            if (cpmsSettings == null) throw new Exception("Could not bind the cpms settings, please check configuration");
            var identitySettings = Configuration.GetSection(IdentitySettings.SectionName).Get<IdentitySettings>();
            if (identitySettings == null) throw new Exception("Could not bind the identity settings, please check configuration");
            var clientsSettings = Configuration.GetSection(ClientsSettings.SectionName).Get<ClientsSettings>();
            if (clientsSettings == null) throw new Exception("Could not bind the Clients Settings, please check configuration");
            var emailSettings = Configuration.GetSection(EmailSettings.SectionName).Get<EmailSettings>();
            if (emailSettings == null) throw new Exception("Could not bind the email settings, please check configuration");

            services.AddSingleton(awsSettings);
            services.AddSingleton(cpmsSettings);
            services.AddSingleton(identitySettings);
            services.AddSingleton(clientsSettings);
            services.AddSingleton(emailSettings);

            services.AddApiVersioning(opts =>
            {
                opts.AssumeDefaultVersionWhenUnspecified = true;
                opts.DefaultApiVersion = ApiVersion.Parse("1");
                opts.ApiVersionReader = ApiVersionReader.Combine
                (
                    new MediaTypeApiVersionReader("version"),
                    new HeaderApiVersionReader("x-dte-version")
                );
                opts.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddSwaggerGen(c =>
            {
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement { { jwtSecurityScheme, Array.Empty<string>() } });

                var versionProvider = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();
                foreach (var description in versionProvider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName, new OpenApiInfo { Title = "Dte.Study.Api", Version = description.GroupName });
                }
                
                var filePath = Path.Combine(AppContext.BaseDirectory, "StudyApi.xml");
                c.IncludeXmlComments(filePath);
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer("AnyAuthenticatedUser", options =>
                {
                    var issuerAuthority = $"https://cognito-idp.{awsSettings.CognitoRegion}.amazonaws.com/{awsSettings.CognitoPoolId}";

                    options.Authority = issuerAuthority;
                    options.RequireHttpsMetadata = true;
                    options.IncludeErrorDetails = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RoleClaimType = "cognito:groups",
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = issuerAuthority,
                        ValidAudiences = awsSettings.CognitoAppClientIds,
                        IssuerValidator = (issuer, token, parameters) =>
                        {
                            if (string.IsNullOrWhiteSpace(token.Issuer)) throw new SecurityTokenInvalidIssuerException("The token issuer is empty or null");
                            var validIssuers = new[] { parameters.ValidIssuer };
                            if (validIssuers.Contains(issuer)) return issuer;
                            throw new SecurityTokenInvalidIssuerException("The sign-in user's account does not belong to the issuer");
                        },
                        AudienceValidator = (audiences, token, parameters) =>
                        {
                            if (token is JwtSecurityToken jwt)
                            {
                                var tokenUse = jwt.Claims.FirstOrDefault(x => x.Type == "token_use");
                                if (string.IsNullOrWhiteSpace(tokenUse?.Value) || !string.Equals(tokenUse.Value, "id", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    return true;
                                }
                            }

                            var audienceList = parameters.ValidAudiences.Any() ? parameters.ValidAudiences : awsSettings.CognitoAppClientIds;

                            return audienceList.Any(audiences.Contains);
                        }
                    };

                    options.Validate();
                });

            services.AddAuthorization(options =>
            {
                var allRoles = typeof(AppRoles).GetFields().Select(x => x.GetValue(null)?.ToString());
                var scopes = new[] { AppScopes.TokenParse };

                // No roles
                options.AddPolicy("AnyAuthenticatedUser", builder => builder
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes("AnyAuthenticatedUser")
                );

                // For specified roles
                options.AddPolicy("Admin", builder => builder
                    .RequireAuthenticatedUser()
                    .RequireRole(AppRoles.Admin)
                    .AddAuthenticationSchemes("AnyAuthenticatedUser")
                );

                options.AddPolicy("Lead", builder => builder
                    .RequireAuthenticatedUser()
                    .RequireRole(AppRoles.Admin, AppRoles.Lead) // TODO - Admin might not have access to lead
                    .AddAuthenticationSchemes("AnyAuthenticatedUser")
                );

                //Scopes
                options.AddPolicy("TokenReadWrite", builder => builder
                    .RequireScopes(scopes)
                    .RequireAuthenticatedUser()
                    .RequireRole(AppRoles.Lead)
                    .AddAuthenticationSchemes("AnyAuthenticatedUser")
                );
            });

            // Applications / Features
            services.AddApplication();
            services.AddInfrastructure(Configuration, Environment.EnvironmentName);
            services.AddMessaging(Configuration);

            // All others
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            // ASP.NET Core setup
            services.AddControllers(x =>
                {
                    x.Filters.Add(typeof(RequestModelValidatorFilter));
                    x.Filters.Add(new AuthorizeFilter("AnyAuthenticatedUser"));
                })
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>(lifetime: ServiceLifetime.Transient))
                .AddFluentValidation(x => x.RegisterValidatorsFromAssembly(Assembly.Load("Application"), lifetime: ServiceLifetime.Transient))
                .AddNewtonsoftJson(x => { x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; })
                .AddNewtonsoftJson();
            
            var build = System.Environment.GetEnvironmentVariable("DTE_BUILD_STRING") ?? "Unknown";
            services.AddHealthChecks()
                .AddCheck("StudyApi", () => HealthCheckResult.Healthy($"Build: {build}"))
                .AddCheck<StudyManagementServiceHealthCheck>("StudyManagementService", timeout: clientsSettings.StudyManagementService.DefaultTimeout, tags: new List<string> { "services" })
                .AddCheck<ParticipantServiceHealthCheck>("ParticipantService", timeout: clientsSettings.ParticipantService.DefaultTimeout, tags: new List<string> { "services" })
                .AddCheck<LocationServiceHealthCheck>("LocationService", timeout: clientsSettings.LocationService.DefaultTimeout, tags: new List<string> { "services" })
                .AddCheck<ReferenceDataServiceHealthCheck>("ReferenceDataService", timeout: clientsSettings.ReferenceDataService.DefaultTimeout, tags: new List<string> { "services" });
        }

        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseCustomExceptionHandler();
            app.UseCustomHeaderForwarderHandler();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"./{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app
                .UseHsts()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapCustomHealthCheck();
                    endpoints.MapControllers();
                    endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Study API V0.1.2"); });
                });
        }
    }
}