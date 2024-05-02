using Microsoft.Extensions.Hosting;

namespace NIHR.Infrastructure.Lambda
{
    public class LambdaHost : IHost
    {
        public static LambdaHostApplicationBuilder CreateBuilder(LambdaApplicationOptions options) => new LambdaHostApplicationBuilder(options);


        private IHost _host;

        public LambdaHost(IHost host) => _host = host;

        public IServiceProvider Services => _host.Services;


        public async Task StartAsync(CancellationToken cancellationToken = default) => await _host.StartAsync(cancellationToken);

        public async Task StopAsync(CancellationToken cancellationToken = default) => await _host.StopAsync(cancellationToken);

        public void Dispose() => _host.Dispose();
    }
}