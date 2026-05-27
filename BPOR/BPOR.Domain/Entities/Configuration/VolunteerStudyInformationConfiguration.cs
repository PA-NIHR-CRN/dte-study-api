using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BPOR.Domain.Entities.Configuration;

public class VolunteerStudyInformationConfiguration : IEntityTypeConfiguration<VolunteerStudyInformation>
{
    public void Configure(EntityTypeBuilder<VolunteerStudyInformation> builder)
    {
        
        builder.HasOne(vsi => vsi.Study)
            .WithMany(c => c.VolunteerStudyInformation)
            .HasForeignKey(c => c.StudyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(vsi => vsi.Contacts)
            .WithOne(c => c.VolunteerStudyInformation)
            .HasForeignKey(c => c.VolunteerStudyInformationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(vsi => vsi.Groups)
            .WithOne(g => g.VolunteerStudyInformation)
            .HasForeignKey(g => g.VolunteerStudyInformationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(vsi => vsi.Sites)
            .WithOne(g => g.VolunteerStudyInformation)
            .HasForeignKey(g => g.VolunteerStudyInformationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(i => i.Description).HasMaxLength(1000);
        builder.Property(i => i.WhatYouWillDo).HasMaxLength(200);
        builder.Property(i => i.IncentiveDetails).HasMaxLength(200);
        builder.Property(i => i.NumberOfVisits).HasMaxLength(200);
        builder.Property(i => i.StudyDuration).HasMaxLength(200);
        builder.Property(i => i.StudyFormat).HasMaxLength(200);
        builder.Property(i => i.OtherDetails).HasMaxLength(200);
        builder.Property(i => i.ExternalWebsiteUrl).Url();
        builder.Property(i => i.InfoToRegisterByEmail).HasMaxLength(200);
        builder.Property(i => i.StagedPreScreenerUrl).Url();
    }
}