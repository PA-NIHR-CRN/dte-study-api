using BPOR.Geolocation.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddNihrConfiguration(builder.Services, builder.Environment);
builder.Services.RegisterServices(builder.Configuration, builder.Environment);

var app = builder.Build();

app.ConfigureSwagger(builder.Environment);

app.UseApplicationMiddleware();

app.Run();
