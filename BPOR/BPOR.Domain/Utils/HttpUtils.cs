using Microsoft.AspNetCore.Http;

namespace BPOR.Domain.Utils;

public static class HttpUtils
{
    public static bool IsSuccessStatusCode(int httpStatusCode) =>
        httpStatusCode is >= StatusCodes.Status200OK and < StatusCodes.Status300MultipleChoices;
}
