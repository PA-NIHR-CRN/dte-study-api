using Contentful.AspNetCore;
using Contentful.Core.Configuration;
using Contentful.Core;
using BPOR.Content;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure.Interfaces;
using Westwind.AspNetCore.Markdown;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<ContentSettings>(Configuration.GetSection("Content"));
        services.Configure<GoogleAnalyticsSettings>(Configuration.GetSection("GoogleAnalytics"));

        services.AddContentful(Configuration);

        services.AddKeyedTransient<IContentfulClient>("preview", (sp, key) =>
        {
            ContentfulOptions value2 = sp.GetService<IOptionsSnapshot<ContentfulOptions>>().Value;
            value2.UsePreviewApi = true;

            HttpClient service2 = sp.GetService<HttpClient>();
            return new ContentfulClient(service2, value2);
        });

        // Add services to the container.
        services.AddControllersWithViews();

        services.Configure<RequestLocalizationOptions>(options =>
         {
             var supportedCultures = new[] { "en-GB", "cy-GB" };
             options.SetDefaultCulture(supportedCultures[0])
                 .AddSupportedCultures(supportedCultures)
                 .AddSupportedUICultures(supportedCultures);
             options.ApplyCurrentCultureToResponseHeaders = true;
         });

        services.AddMarkdown();
        services.AddHttpContextAccessor();
        services.AddScoped<IContentProvider, StaticContentProvider>();

        services
            .AddMvc()
            .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseRequestLocalization();

        app.UseEndpoints(endpoints => {
            endpoints.MapControllerRoute(
            name: "BPoR",
            pattern: "{controller=Home}/{action=Index}/{id?}",
            defaults: new { controller = "Home" });

            endpoints.MapControllerRoute(
            name: "JDR",
            pattern: "healthcare/{action=Index}/{id?}",
            defaults: new { controller = "Healthcare" });
        });

        app.UseMarkdown();
    }
}