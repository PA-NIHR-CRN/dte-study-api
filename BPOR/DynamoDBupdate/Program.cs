using DynamoBDupdate.Startup;
using DynamoDBupdate.Backfills;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

Console.WriteLine("Backfill starting up...");

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.AddNihrConfiguration();
builder.Services.RegisterServices(builder.Configuration, builder.Environment);

var app = builder.Build();

using var scope = app.Services.CreateScope();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Program.cs: Logger initialized.");
Console.WriteLine("Program.cs: Logger initialized.");

try
{
    var backfill = scope.ServiceProvider.GetRequiredService<Backfill>();
    logger.LogInformation("Backfill.RunAsync starting...");
    Console.WriteLine("Calling Backfill.RunAsync...");

    await backfill.RunAsync(true, true, CancellationToken.None);

    logger.LogInformation("Backfill.RunAsync completed.");
    Console.WriteLine("Backfill.RunAsync completed.");
}
catch (Exception ex)
{
    logger.LogError(ex, "Backfill failed with an exception.");
    Console.WriteLine($"Exception: {ex.Message}");
}

app.Run();