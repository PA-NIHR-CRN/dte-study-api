using System.Net;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Helpers;

public static class HttpStatusCodeHelper
{
    public static bool IsSuccess(HttpStatusCode statusCode)
    {
        return ((int)statusCode >= StatusCodes.Status200OK) &&
               ((int)statusCode < StatusCodes.Status300MultipleChoices);
    }
}
