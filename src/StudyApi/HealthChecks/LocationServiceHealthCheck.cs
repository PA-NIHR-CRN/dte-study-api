using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dte.Common.Exceptions;
using Dte.Location.Api.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace StudyApi.HealthChecks
{
    public class LocationServiceHealthCheck : IHealthCheck
    {
        private readonly ILocationApiClient _client;

        public LocationServiceHealthCheck(ILocationApiClient client)
        {
            _client = client;
        }
        
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var data = new Dictionary<string, object>
            {
                { "timeout", context.Registration.Timeout },
            };

            var sw = Stopwatch.StartNew();

            HealthCheckResult healthCheckResult;
            try
            {
                var result = await _client.GetHealthAsync(true);
                sw.Stop();

                data.Add("time", sw.Elapsed);
                data.Add("httpStatus", HttpStatusCode.OK);
                data.Add("httpStatusCode", (int)HttpStatusCode.OK);

                var isHealthy = result != null;

                healthCheckResult = isHealthy
                    ? HealthCheckResult.Healthy(data: data)
                    : new HealthCheckResult(context.Registration.FailureStatus, data: data);
            }
            catch (HttpServiceException ex)
            {
                data.Add("time", sw.Elapsed);
                data.Add("exception", ex.GetType().Name);
                data.Add("exceptionMessage", ex.Message);
                data.Add("httpStatus", ex.HttpStatusCode);
                data.Add("httpStatusCode", (int)ex.HttpStatusCode);

                healthCheckResult = new HealthCheckResult(context.Registration.FailureStatus, data: data);
            }
            catch (Exception ex)
            {
                data.Add("time", sw.Elapsed);
                data.Add("exception", ex.GetType().Name);
                data.Add("exceptionMessage", ex.Message);
                
                healthCheckResult = new HealthCheckResult(context.Registration.FailureStatus, data: data);
            }

            return healthCheckResult;
        }
    }
}