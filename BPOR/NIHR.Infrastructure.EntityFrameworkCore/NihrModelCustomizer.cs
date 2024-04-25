using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace NIHR.Infrastructure.EntityFrameworkCore
{
    public class NihrModelCustomizer : IModelCustomizer
    {
        public void Customize(ModelBuilder modelBuilder, DbContext context)
        {
            modelBuilder.EnableSoftDelete(context.GetType().Assembly);
            modelBuilder.EnableReferenceDataEntites(context.GetType().Assembly);
        }
    }
}