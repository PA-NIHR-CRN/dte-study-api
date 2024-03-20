using BPOR.Rms.Startup;
using NIHR.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddNihrConfiguration(builder.Services, builder.Environment);
builder.Services.AddIdgAuthentication(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

app.UseApplicationMiddleware();

app.Run();
