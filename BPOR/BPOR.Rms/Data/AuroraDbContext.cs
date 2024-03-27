using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BPOR.Domain.Entities
{
    public class AuroraDbContext : DbContext
    {
        public AuroraDbContext (DbContextOptions<AuroraDbContext> options)
            : base(options)
        {
        }

        public DbSet<BPOR.Domain.Entities.Study> Study { get; set; } = default!;
        public DbSet<BPOR.Domain.Entities.AuroraParticipant> AuroraParticipant { get; set; } = default!;
    }
}
