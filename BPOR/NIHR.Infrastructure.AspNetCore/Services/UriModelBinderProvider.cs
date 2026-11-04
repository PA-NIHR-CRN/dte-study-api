using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NIHR.Infrastructure.AspNetCore.Services;

internal class UriModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        return context.Metadata.ModelType == typeof(Uri) ? new UriModelBinder() : null;
    }
}