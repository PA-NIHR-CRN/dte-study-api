using DynamoBDupdate.Startup;
using DynamoDBupdate.Backfills;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddNihrConfiguration(builder.Services, builder.Environment);
builder.Services.RegisterServices(builder.Configuration, builder.Environment);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

var cts = new CancellationTokenSource();

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    logger.LogInformation("Starting backfill RunAsync call.");

    var backfill = scope.ServiceProvider.GetRequiredService<Backfill>();

    await backfill.RunAsync(true, true, cts.Token);

    logger.LogInformation("Completed backfill RunAsync call.");
}

app.Run();