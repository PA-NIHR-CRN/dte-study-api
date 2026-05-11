using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain;

public static class EntityBuilderHelper
{
    public static PropertyBuilder<string> Url(this PropertyBuilder<string> builder)
        => builder.HasMaxLength(UrlMaxLength);

    public const int UrlMaxLength = 2048;
}