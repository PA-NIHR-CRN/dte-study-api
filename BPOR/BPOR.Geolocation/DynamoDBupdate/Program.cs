using DynamoBDupdate.Startup;
using DynamoDBupdate.CRNCC2563Stage2Backfill;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddNihrConfiguration(builder.Services, builder.Environment);
builder.Services.RegisterServices(builder.Configuration, builder.Environment);

var app = builder.Build();

var cts = new CancellationTokenSource();

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<Stage2Backfill>().RunAsync(cts.Token);
}