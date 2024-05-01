using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NIHR.Infrastructure.Configuration;

namespace BPOR.Registration.Stream.Handler
{
    public class LambdaHostApplicationBuilder : IHostApplicationBuilder
    {
        private readonly ServiceCollection _services;
        private readonly LambdaHostEnvironment _hostEnvironment;
        private IDictionary<object, object> _properties;
        private ConfigurationManager _configuration;

        public LambdaHostApplicationBuilder(string applicationName)
        {
            _services = new ServiceCollection();

            _hostEnvironment = new LambdaHostEnvironment(
                System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                applicationName,
                Directory.GetCurrentDirectory(), null);

            _properties = new Dictionary<object, object>();

            _configuration = new ConfigurationManager();

            _services.AddSingleton(_configuration);
        }

        public IDictionary<object, object> Properties => _properties;

        public IConfigurationManager Configuration => _configuration;

        public IHostEnvironment Environment => _hostEnvironment;

        public ILoggingBuilder Logging => throw new NotImplementedException();

        public IMetricsBuilder Metrics => throw new NotImplementedException();

        public IServiceCollection Services => _services;

        public void ConfigureContainer<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory, Action<TContainerBuilder>? configure = null) where TContainerBuilder : notnull
        {
            
        }

        public Lambda Build()
        {
            return new Lambda(_services.BuildServiceProvider());
        }
    }
}