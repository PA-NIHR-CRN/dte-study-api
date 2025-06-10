using BPOR.Domain.Entities.Configuration;
using BPOR.Rms.Startup;
using Microsoft.AspNetCore.Authorization;
using DynamoDBupdate.Backfills;
using RMSStartup = BPOR.Rms.Startup.DependencyInjection;
using DynamoStartup = DynamoBDupdate.Startup.DependencyInjection;

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

RMSStartup.RegisterServices(builder.Services, builder.Configuration, builder.Environment);
DynamoStartup.RegisterServices(builder.Services, builder.Configuration, builder.Environment);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var backfill = scope.ServiceProvider.GetRequiredService<Backfill>();

    logger.LogInformation("🟡 Starting DynamoDB backfill from BPOR.Rms...");

    try
    {
        await backfill.RunAsync(
            runStage2Backfill: true,
            runCanonicalTownBackfill: true,
            cancellationToken: CancellationToken.None
        );

        logger.LogInformation("✅ Backfill completed.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "❌ Backfill failed.");
    }
}


app.ConfigureSwagger(builder.Environment);

app.UseApplicationMiddleware();

app.Run();