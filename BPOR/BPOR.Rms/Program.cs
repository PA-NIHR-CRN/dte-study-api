using BPOR.Domain.Entities.Configuration;
using BPOR.Rms.Startup;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication
    .CreateBuilder(args);

builder.AddNihrConfiguration();

builder.AddIdgAuthentication(authOptions =>
    {
        authOptions.FallbackPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireRole(RoleConfiguration.GetRoles().Select(x => x.Code))
            .Build();
    }
);

builder.AddAWSSystemsManagerDataProtection("/BPOR/RMS");

builder.Services.RegisterServices(builder.Configuration, builder.Environment);

builder.WebHost.UseStaticWebAssets();

var app = builder.Build();

app.ConfigureSwagger(builder.Environment);

app.UseApplicationMiddleware();

app.Run();
