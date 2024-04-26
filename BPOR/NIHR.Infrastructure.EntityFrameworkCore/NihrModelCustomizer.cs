using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace NIHR.Infrastructure.EntityFrameworkCore
{
    public class NihrModelCustomizer : RelationalModelCustomizer
    {
        public NihrModelCustomizer(ModelCustomizerDependencies dependencies) : base(dependencies)
        {
        }

        public override void Customize(ModelBuilder modelBuilder, DbContext context)
        {
            base.Customize(modelBuilder, context);

            modelBuilder.EnableSoftDelete(context.GetType().Assembly);
            modelBuilder.EnableReferenceDataEntites(context.GetType().Assembly);
        }
    }
}