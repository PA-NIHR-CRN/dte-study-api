using BPOR.Registration.Api.Startup;
using NIHR.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddNihrConfiguration(builder.Environment);

builder.Services.AddEndpointsApiExplorer().AddSwaggerGen().ConfigureNihrLogging(builder.Configuration)
    .RegisterServices(builder.Configuration, builder.Environment).AddAuthentication(builder.Environment);

var app = builder.Build();

app.ConfigureSwagger().UseApplicationMiddleware(builder.Environment).Run();
