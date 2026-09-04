using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NIHR.Infrastructure.EntityFrameworkCore.Extensions;

public static class PropertyBuilderExtensions
{
    public const int NameMaxLength = 255;
    public const int UrlMaxLength = 2048;
    public const int EmailMaxLength = 255;
    
    public static PropertyBuilder<string?> Email(
        this PropertyBuilder<string?> propertyBuilder)
    {
        return propertyBuilder.HasMaxLength(EmailMaxLength);
    }

    public static PropertyBuilder<string?> Url(
        this PropertyBuilder<string?> propertyBuilder)
    {
        return propertyBuilder.HasMaxLength(UrlMaxLength);
    }

    public static PropertyBuilder<string?> Name(
        this PropertyBuilder<string?> propertyBuilder)
    {
        return propertyBuilder.HasMaxLength(NameMaxLength);
    }
}