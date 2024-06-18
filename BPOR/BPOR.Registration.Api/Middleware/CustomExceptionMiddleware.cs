using System.Net;
using BPOR.Infrastructure.Exceptions;
using Dte.Common.Exceptions;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Newtonsoft.Json;

namespace BPOR.Registration.Api.Middleware;

public class CustomExceptionMiddleware(
    RequestDelegate next,
    IHeaderService headerService,
    ILogger<CustomExceptionMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (ex)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(validationException.Failures);
                logger.LogWarning(result);
                break;
            case BadRequestException badRequestException:
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new { badRequestException.Message });
                logger.LogWarning(result);
                break;
            case NotFoundException _:
                code = HttpStatusCode.NotFound;
                break;
            case UnauthorizedException _:
                code = HttpStatusCode.Unauthorized;
                break;
            case CognitoPhoneNumberUpdateException:
                code = HttpStatusCode.BadRequest;
                break;
            default:
                result = JsonConvert.SerializeObject(new
                    { error = code, conversationId = headerService.GetConversationId() });
                logger.LogError(ex, result);
                logger.LogError(ex.StackTrace);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}
