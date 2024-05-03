using BPOR.Rms.Startup;
using NIHR.Infrastructure.Configuration;
using System.Diagnostics;

var builder = WebApplication
    .CreateBuilder(args);

builder.AddNihrConfiguration();
builder.AddIdgAuthentication();

builder.Services.RegisterServices(builder.Configuration, builder.Environment);

var dataprotection = builder.Services.AddDataProtection();

if (!(builder.Environment.IsDevelopment() && Debugger.IsAttached))
{
    dataprotection.PersistKeysToAWSSystemsManager("/BPOR/RMS");
}

var app = builder.Build();

app.ConfigureSwagger(builder.Environment);

app.UseApplicationMiddleware();

app.UseMiddleware<IdgAuthenticationMiddleware>();

app.UseCookiePolicy(
    new CookiePolicyOptions
    {
        Secure = Debugger.IsAttached && !app.Environment.IsProduction() ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.Always,
        OnAppendCookie = context =>
        {
            Debug.WriteLine((context.CookieName));
        }
    });

app.Run();
