using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace NIHR.Infrastructure.EntityFrameworkCore;

public static class SoftDeleteQueryFilters
{
    public static void EnableSoftDelete(this ModelBuilder modelBuilder, System.Reflection.Assembly assembly)
    {
        var softDeleteEntities = typeof(ISoftDelete).Assembly.GetTypes().Where(type => typeof(ISoftDelete).IsAssignableFrom(type)
                            && type.IsClass
                            && !type.IsAbstract);

        var subClassSoftDeleteEntities = assembly.GetTypes().Where(type => typeof(ISoftDelete).IsAssignableFrom(type)
                            && type.IsClass
                            && !type.IsAbstract); ;

        softDeleteEntities = softDeleteEntities.Union(subClassSoftDeleteEntities).DistinctBy(x => x.FullName);

        foreach (var softDeleteEntity in softDeleteEntities)
        {
            modelBuilder.Entity(softDeleteEntity).HasQueryFilter(GenerateQueryFilterLambda(softDeleteEntity));
        }
    }

    private static LambdaExpression? GenerateQueryFilterLambda(Type type)
    {
        // Generates expression equivalent to
        // x => x.IsDeleted == false;

        var parameter = Expression.Parameter(type, "x");
        var falseConstantValue = Expression.Constant(false);
        var propertyAccess = Expression.PropertyOrField(parameter, nameof(ISoftDelete.IsDeleted));
        var equalExpression = Expression.Equal(propertyAccess, falseConstantValue);
        var lambda = Expression.Lambda(equalExpression, parameter);

        return lambda;
    }

}
