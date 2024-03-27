using BPOR.Rms.Startup;
using NIHR.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using BPOR.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AuroraDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AuroraDbContext") ??
                      throw new InvalidOperationException("Connection string 'AuroraDbContext' not found.")));

builder.Configuration.AddNihrConfiguration(builder.Services, builder.Environment);
builder.Services.AddIdgAuthentication(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration, builder.Environment);

var app = builder.Build();

app.ConfigureSwagger(builder.Environment);

app.UseApplicationMiddleware();

app.Run();
