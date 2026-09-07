using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NIHR.Infrastructure.EntityFrameworkCore.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> ConfigureReferenceData<TEntity, TId>(
        this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IReferenceData<TId>
        where TId : struct, Enum
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Description)
            .HasMaxLength(255);

        builder.Property(x => x.IsDeleted)
            .IsRequired();

        return builder;
    }
}