using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace NIHR.Infrastructure.EntityFrameworkCore.Internal
{
    internal class NihrConventionSetPlugin : IConventionSetPlugin
    {
        public ConventionSet ModifyConventions(ConventionSet conventionSet)
        {
            // TODO: add configuration option to enable/disable below
            // conventionSet.Remove(typeof(TableNameFromDbSetConvention));

            conventionSet.Add(new ReferenceDataConvention());
            conventionSet.Add(new SoftDeleteConvention());

            return conventionSet;
        }
    }
}