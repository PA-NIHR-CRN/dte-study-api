using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace StudyApi.Common
{
    public class RequestModelValidatorFilter : IAsyncActionFilter
    {
        private readonly ILogger<RequestModelValidatorFilter> _logger;
        private readonly IWebHostEnvironment _environment;

        public RequestModelValidatorFilter(ILogger<RequestModelValidatorFilter> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)).ToList();
                var errorsString = string.Join("; ", errors);
                _logger.LogWarning("Request Validation Failed: {Errors}", errorsString);

                // Set a generic error message if not in development
                if (!_environment.IsDevelopment())
                {
                    errorsString = "Your request contains invalid parameters. Please check and try again.";
                }
                
                context.Result = new BadRequestObjectResult(errorsString);
            }
            else
            {
                await next();
            }
        }
    }
}
