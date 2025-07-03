using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using AWS.Lambda.Powertools.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using StudyApi.Extensions;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace StudyApi
{
    public class Function : Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();

                    logging.AddPowertoolsLogger(
                        options =>
                        {
                            options.Service = "BPoR VS Study API";
                        });
                })
                .UseStartup<Startup>()
                .AddAwsSecrets();
        }

        // Needed for now to be able to deploy locally so the configuration can find the function
        [Logging(ClearState = true)]
        public override Task<APIGatewayProxyResponse> FunctionHandlerAsync(APIGatewayProxyRequest request, ILambdaContext lambdaContext)
        {
            return base.FunctionHandlerAsync(request, lambdaContext);
        }
    }
}