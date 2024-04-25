using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NIHR.Infrastructure.EntityFrameworkCore;

public static class ReferenceDataExtensions
{
    public static void EnableReferenceDataEntites(this ModelBuilder modelBuilder, Assembly subclassAssembly, string tableNamePrefix = "SysRef")
    {
        var refDataTypes = typeof(IReferenceData).Assembly.GetReferenceDataEntities();

        refDataTypes = refDataTypes.Union(subclassAssembly.GetReferenceDataEntities()).DistinctBy(x => x.FullName);

        foreach (var type in refDataTypes)
        {
            if (!type.Name.StartsWith(tableNamePrefix))
            {
                modelBuilder.Entity(type).ToTable(tableNamePrefix + type.Name);
            }
        }
    }

    private static IEnumerable<Type> GetReferenceDataEntities(this Assembly assembly)
    {
        return assembly.GetTypes().Where(t => typeof(IReferenceData).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
    }
}
