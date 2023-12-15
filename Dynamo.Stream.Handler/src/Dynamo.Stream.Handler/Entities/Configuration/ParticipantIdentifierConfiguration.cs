using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
