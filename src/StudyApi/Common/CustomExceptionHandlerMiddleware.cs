using System;
using System.Net;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using Dte.Common.Exceptions;
using Dte.Common.Extensions;
using Dte.Common.Http;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace StudyApi.Common
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHeaderService _headerService;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, IHeaderService headerService, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _headerService = headerService;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
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
                    _logger.LogWarning("{@Failures}", validationException.Failures);
                    break;
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(new { badRequestException.Message});
                    _logger.LogWarning("{@Message}", badRequestException.Message);
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
                    var error = new { error = code, conversationId = _headerService.GetConversationId() };
                    result = JsonConvert.SerializeObject(error);
                    _logger.LogError(ex, "@{error}", error);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
           
            return context.Response.WriteAsync(result);
        }
    }
}