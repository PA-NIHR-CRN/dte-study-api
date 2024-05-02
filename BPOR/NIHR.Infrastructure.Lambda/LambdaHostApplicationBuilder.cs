using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NIHR.Infrastructure.Lambda
{
    public class LambdaHostApplicationBuilder : IHostApplicationBuilder
    {
        private HostApplicationBuilder _hostApplicationBuilder;

        public LambdaHostApplicationBuilder(LambdaApplicationOptions options)
        {
            var configuration = new ConfigurationManager();

            configuration.AddEnvironmentVariables(prefix: "ASPNETCORE_");

            _hostApplicationBuilder = new HostApplicationBuilder(new HostApplicationBuilderSettings
            {
                Args = options.Args,
                ApplicationName = options.ApplicationName,
                EnvironmentName = options.EnvironmentName,
                ContentRootPath = options.ContentRootPath,
                Configuration = configuration,
            });

            Services.AddSingleton(configuration);
        }

        IDictionary<object, object> IHostApplicationBuilder.Properties => ((IHostApplicationBuilder)_hostApplicationBuilder).Properties;

        public IConfigurationManager Configuration => _hostApplicationBuilder.Configuration;

        public IHostEnvironment Environment => _hostApplicationBuilder.Environment;

        public ILoggingBuilder Logging => _hostApplicationBuilder.Logging;

        public IMetricsBuilder Metrics => _hostApplicationBuilder.Metrics;

        public IServiceCollection Services => _hostApplicationBuilder.Services;

        public void ConfigureContainer<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory, Action<TContainerBuilder>? configure = null) where TContainerBuilder : notnull
        => _hostApplicationBuilder.ConfigureContainer(factory, configure);

        public LambdaHost Build()
        {
            return new LambdaHost(_hostApplicationBuilder.Build());
        }
    }
}