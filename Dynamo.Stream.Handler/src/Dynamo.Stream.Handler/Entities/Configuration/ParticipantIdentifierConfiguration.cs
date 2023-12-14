using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo.Stream.Handler.Entities.Configuration
{
    public class ParticipantIdentifierConfiguration : IEntityTypeConfiguration<ParticipantIdentifier>
    {
        public void Configure(EntityTypeBuilder<ParticipantIdentifier> builder)
        {
            builder.HasIndex(x => new { x.Value });
        }
    }
}
