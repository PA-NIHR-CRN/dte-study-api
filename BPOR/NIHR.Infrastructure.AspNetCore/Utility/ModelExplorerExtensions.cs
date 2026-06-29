using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace NIHR.Infrastructure.AspNetCore;

public static class ModelExplorerExtensions
{
    public static string GetDisplayString(this ModelExpression model)
    {
        
        if (model.Metadata is DefaultModelMetadata defaultModelMetadata)
        {
            var att = defaultModelMetadata.Attributes.Attributes.OfType<ValueDisplayFormatterAttribute>().FirstOrDefault();
            if (att != null)
            {
                var formatter = Activator.CreateInstance(att.Type);
                if (formatter is IDisplayStringFormatter displayStringFormatter)
                {
                    return displayStringFormatter.ToDisplayString(model.Model);
                }
                
                throw new InvalidOperationException($"{att.Type} does not implement IDisplayStringFormatter");
            }
        }

        return model.Model?.ToString() ?? string.Empty;
    }
}