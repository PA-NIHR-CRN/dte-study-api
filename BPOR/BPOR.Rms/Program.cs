using BPOR.Rms.Startup;
using NIHR.Infrastructure.Configuration;

var builder = WebApplication
    .CreateBuilder(args);

builder.AddNihrConfiguration();
builder.AddIdgAuthentication();
builder.AddAWSSystemsManagerDataProtection("/BPOR/RMS");

builder.Services.RegisterServices(builder.Configuration, builder.Environment);

var app = builder.Build();

app.ConfigureSwagger(builder.Environment);

app.UseApplicationMiddleware();

app.UseMiddleware<IdgAuthenticationMiddleware>();

app.Run();
