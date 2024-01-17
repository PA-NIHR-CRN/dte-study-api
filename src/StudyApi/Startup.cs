using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Settings;
using AspNetCoreRateLimit;
using Dte.Common;
using Dte.Common.Authentication;
using FluentValidation.AspNetCore;
using Infrastructure.Clients;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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
        private IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuration
            var contentfulSettings = Configuration.GetSection(ContentfulSettings.SectionName).Get<ContentfulSettings>();
            var appSettings = Configuration.GetSection(AppSettings.SectionName).Get<AppSettings>();
            var awsSettings = Configuration.GetSection(AwsSettings.SectionName).Get<AwsSettings>();
            if (awsSettings == null) throw new Exception("Could not bind the aws settings, please check configuration");
            var clientsSettings = Configuration.GetSection(ClientsSettings.SectionName).Get<ClientsSettings>();
            if (clientsSettings == null)
                throw new Exception("Could not bind the Clients Settings, please check configuration");
            var emailSettings = Configuration.GetSection(EmailSettings.SectionName).Get<EmailSettings>();
            if (emailSettings == null)
                throw new Exception("Could not bind the email settings, please check configuration");

            services.AddSingleton(awsSettings);
            services.AddSingleton(clientsSettings);
            services.AddSingleton(emailSettings);
            services.AddSingleton(appSettings);
            services.AddSingleton(contentfulSettings);              

            services.AddTransient(provider => Configuration);

            services.Configure<NhsLoginSettings>(Configuration.GetSection("NhsLogin"));

            services.AddHttpClient<NhsLoginHttpClient>((serviceProvider, httpClient) =>
            {
                var settings = serviceProvider.GetService<IOptions<NhsLoginSettings>>()?.Value;

                httpClient.BaseAddress = new Uri(settings.BaseUrl);
            });

            if (Environment.IsDevelopment())
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(name: "AllowLocal",
                        policy =>
                        {
                            policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod()
                                .AllowCredentials();
                        });
                });
            }

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
                var versionProvider = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();
                foreach (var description in versionProvider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName,
                        new OpenApiInfo { Title = "Dte.Study.Api", Version = description.GroupName });
                }

                var filePath = Path.Combine(AppContext.BaseDirectory, "StudyApi.xml");
                c.IncludeXmlComments(filePath);
            });

            var dataprotection = services.AddDataProtection();
            if (!Environment.IsDevelopment())
            {
                dataprotection.PersistKeysToAWSSystemsManager("/BPOR/DataProtection");
            }

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = ".BPOR.Session";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                    options.Cookie.SameSite = Environment.IsDevelopment() ? SameSiteMode.None : SameSiteMode.Strict;

                    options.Events.OnRedirectToLogin = (context) =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
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
                );

                // For specified roles
                options.AddPolicy("Admin", builder => builder
                    .RequireAuthenticatedUser()
                    .RequireRole(AppRoles.Admin)
                );

                options.AddPolicy("Lead", builder => builder
                        .RequireAuthenticatedUser()
                        .RequireRole(AppRoles.Admin, AppRoles.Lead) // TODO - Admin might not have access to lead
                );

                //Scopes
                options.AddPolicy("TokenReadWrite", builder => builder
                    .RequireScopes(scopes)
                    .RequireAuthenticatedUser()
                    .RequireRole(AppRoles.Lead)
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
                .AddFluentValidation(x =>
                    x.RegisterValidatorsFromAssemblyContaining<Startup>(lifetime: ServiceLifetime.Transient))
                .AddFluentValidation(x =>
                    x.RegisterValidatorsFromAssembly(Assembly.Load("Application"), lifetime: ServiceLifetime.Transient))
                .AddNewtonsoftJson(x => { x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; })
                .AddNewtonsoftJson().ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            var build = System.Environment.GetEnvironmentVariable("DTE_BUILD_STRING") ?? "Unknown";
            services.AddHealthChecks()
                .AddCheck("StudyApi", () => HealthCheckResult.Healthy($"Build: {build}"))
                .AddCheck<LocationServiceHealthCheck>("LocationService",
                    timeout: clientsSettings.LocationService.DefaultTimeout, tags: new List<string> { "services" });
        }

        private static void SetSessionExpiryCookie(AppendCookieContext context)
        {
            var issuedAt = DateTimeOffset.UtcNow;
            var expiresAt = issuedAt.Add(TimeSpan.FromMinutes(10));

            var content = JsonConvert.SerializeObject(
                new { issuedAt, expiresAt },
                new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat }
            );

            if (string.IsNullOrWhiteSpace(context.CookieValue))
            {
                context.Context.Response.Cookies.Delete(context.CookieName + ".Expiry");
            }
            else
            {
                context.Context.Response.Cookies.Append(
                    context.CookieName + ".Expiry",
                    content,
                    new CookieOptions
                    {
                        HttpOnly =
                            false, // Cookie must be accessible from the client to allow session expiry notification
                        Secure = context.CookieOptions.Secure,
                        SameSite = context.CookieOptions.SameSite,
                        IsEssential = true
                    });
            }
        }

        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseIpRateLimiting();

            app.UseCustomExceptionHandler();
            app.UseCustomHeaderForwarderHandler();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"./{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });

            app.UseRouting();

            if (Environment.IsDevelopment())
            {
                app.UseCors("AllowLocal");
            }
            

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = Environment.IsDevelopment() ? SameSiteMode.None : SameSiteMode.Strict,
                Secure = Environment.IsDevelopment() ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.Always,
                OnAppendCookie = context =>
                {
                    if (context.CookieName == ".BPOR.Session")
                    {
                        SetSessionExpiryCookie(context);
                    }
                }
            });

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<SessionExpiryMiddleware>();

            app.UseWhen(context =>
                // bypass maintainence mode middleware for configuration controller
                !context.Request.Path.StartsWithSegments("/api/configuration"),
                config => config.UseMiddleware<MaintenanceMiddleware>()
            );

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
