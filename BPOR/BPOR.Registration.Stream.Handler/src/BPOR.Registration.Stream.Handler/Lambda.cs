using Microsoft.Extensions.Hosting;

namespace BPOR.Registration.Stream.Handler
{
    public class Lambda : IHost
    {
        public IServiceProvider Services => _services;

        public static LambdaHostApplicationBuilder CreateBuilder(string applicationName)
        {
            return new LambdaHostApplicationBuilder(applicationName);
        }

        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        private IServiceProvider _services;

        public Lambda(IServiceProvider services)
        {
            _services = services;
        }
    }
}