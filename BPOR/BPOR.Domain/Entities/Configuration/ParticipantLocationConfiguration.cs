using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPOR.Domain.Entities.Configuration
{
    public class ParticipantLocationConfiguration : IEntityTypeConfiguration<ParticipantLocation>
    {
        public const int LocationSrid = 4326;
        public void Configure(EntityTypeBuilder<ParticipantLocation> builder)
        {
            builder.Property(x => x.Location).HasSpatialReferenceSystem(LocationSrid);
            builder.HasIndex(x => x.Location).IsSpatial();
        }
    }
}
