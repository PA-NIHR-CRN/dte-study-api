using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NIHR.Infrastructure.AspNetCore.Validation;

namespace NIHR.Infrastructure.AspNetCore.Services;

public class RedirectUrlExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ProhibitedRedirectUrlException)
        {
            context.ExceptionHandled = true;
            context.Result = new BadRequestResult();
        }
    }
}