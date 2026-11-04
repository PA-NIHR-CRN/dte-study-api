using BPOR.Registration.Api.Startup;
using BPOR.Rms.Infrastructure.Configuration.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddNihrConfiguration()
        .ConfigureNihrLogging()
        .RegisterServices()
        .AddAuthentication();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

app.ConfigureSwagger()
    .UseApplicationMiddleware(builder.Environment).Run();
