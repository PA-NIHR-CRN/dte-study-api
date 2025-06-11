using DynamoDBupdate.Backfills;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DynamoDBupdate.Startup;
using NReco.Logging.File;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;


IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
        .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(Math.Pow(2, retry)));
}


var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddNihrConfiguration(builder.Services, builder.Environment);
builder.Services.RegisterServices(builder.Configuration, builder.Environment);
builder.Services.AddScoped<Stage2Backfill>();
builder.Services.AddScoped<CanonicalTownBackfill>();

builder.Services.AddOptions<OsSettings>().BindConfiguration("OsSettings");

builder.Services.AddHttpClient<CanonicalTownBackfill>((sp, c) =>
{
    var osSettings = sp.GetRequiredService<IOptions<OsSettings>>();
    c.BaseAddress = new Uri("https://api.os.uk");
    c.DefaultRequestHeaders.Add("key", osSettings.Value.Key);
})
    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
    .AddPolicyHandler(GetRetryPolicy());

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddFile($"Backfill-{DateTime.Now.ToString("o").Replace(":", string.Empty).Replace("+", string.Empty)}.log", append: true);
});

var app = builder.Build();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using var cts = new CancellationTokenSource();

if (false)
{
    using (var scope = scopeFactory.CreateScope())
    {
        var canonicalTownBackfill = scope.ServiceProvider.GetRequiredService<CanonicalTownBackfill>();

        await canonicalTownBackfill.RunAsync(cts.Token);
    }
}

using (var scope = scopeFactory.CreateScope())
{
    var stage2Backfill = scope.ServiceProvider.GetRequiredService<Stage2Backfill>();

    await stage2Backfill.RunAsync(cts.Token);
}

await app.RunAsync(cts.Token);