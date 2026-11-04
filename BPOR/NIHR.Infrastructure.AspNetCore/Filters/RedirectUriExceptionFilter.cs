using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NIHR.Infrastructure.AspNetCore.Validation;

namespace NIHR.Infrastructure.AspNetCore.Filters;

public class RedirectUriExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ProhibitedRedirectUriException)
        {
            context.ExceptionHandled = true;
            context.Result = new BadRequestResult();
        }
    }
}