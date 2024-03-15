using Microsoft.AspNetCore.Http;

namespace BPOR.Infrastructure.Utils;

public static class HttpUtils
{
    public static bool IsSuccessStatusCode(int httpStatusCode) => httpStatusCode >= StatusCodes.Status200OK &&
                                                                  httpStatusCode < StatusCodes.Status300MultipleChoices;
}
