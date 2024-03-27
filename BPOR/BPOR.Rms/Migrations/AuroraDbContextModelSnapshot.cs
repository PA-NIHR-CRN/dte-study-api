﻿// <auto-generated />
using System;
using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BPOR.Rms.Migrations
{
    [DbContext(typeof(AuroraDbContext))]
    partial class AuroraDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("BPOR.Domain.Entities.AuroraParticipant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CommunicationLanguageId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DailyLifeImpactId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("EthnicBackground")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("EthnicGroup")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int?>("GenderId")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("GenderIsSameAsSexRegisteredAtBirth")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("HasLongTermCondition")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LandlineNumber")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("MobileNumber")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("NHSNumber")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<bool>("RegistrationConsent")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("RegistrationConsentAtUtc")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RemovalOfConsentRegistrationAtUtc")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Stage2CompleteUtc")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CommunicationLanguageId");

                    b.HasIndex("DailyLifeImpactId");

                    b.HasIndex("GenderId");

                    b.ToTable("AuroraParticipant");
                });

            modelBuilder.Entity("BPOR.Domain.Entities.AuroraParticipantAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AddressLine1")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("AddressLine3")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("AddressLine4")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ParticipantId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Postcode")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Town")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantId")
                        .IsUnique();

                    b.ToTable("AuroraParticipantAddress");
                });

            modelBuilder.Entity("BPOR.Domain.Entities.ParticipantHealthCondition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("HealthConditionId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ParticipantId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HealthConditionId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("ParticipantHealthCondition");
                });

            modelBuilder.Entity("BPOR.Domain.Entities.ParticipantIdentifier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdentifierTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ParticipantId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IdentifierTypeId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("ParticipantIdentifier");
                });

            modelBuilder.Entity("BPOR.Domain.Entities.RefData.CommunicationLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("CommunicationLanguage");
                });

            modelBuilder.Entity("BPOR.Domain.Entities.RefData.DailyLifeImpact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("DailyLifeImpact");
                });

            modelBuilder.Entity("BPOR.Domain.Entities.RefData.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Gender");
                });

            modelBuilder.Entity("BPOR.Domain.Entities.RefData.HealthCondition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("HealthCondition");
                });

            modelBuilder.Entity("BPOR.Domain.Entities.RefData.IdentifierType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("IdentifierType");
                });

            modelBuilder.Entity("BPOR.Domain.Entities.SourceReference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ParticipantId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Pk")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantId");

                    b.ToTable("SourceReference");
                });

            modelBuilder.Entity("BPOR.Domain.Entities.Study", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CpmsId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAnonymous")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StudyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Study");
                });

            modelBuilder.Entity("BPOR.Domain.Entities.AuroraParticipant", b =>
                {
                    b.HasOne("BPOR.Domain.Entities.RefData.CommunicationLanguage", "CommunicationLanguage")
                        .WithMany()
                        .HasForeignKey("CommunicationLanguageId");

                    b.HasOne("BPOR.Domain.Entities.RefData.DailyLifeImpact", "DailyLifeImpact")
                        .WithMany()
                        .HasForeignKey("DailyLifeImpactId");

                    b.HasOne("BPOR.Domain.Entities.RefData.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId");

                    b.Navigation("CommunicationLanguage");

                    b.Navigation("DailyLifeImpact");

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("BPOR.Domain.Entities.AuroraParticipantAddress", b =>
                {
                    b.HasOne("BPOR.Domain.Entities.AuroraParticipant", "Participant")
                        .WithOne("Address")
                        .HasForeignKey("BPOR.Domain.Entities.AuroraParticipantAddress", "ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("BPOR.Domain.Entities.ParticipantHealthCondition", b =>
                {
                    b.HasOne("BPOR.Domain.Entities.RefData.HealthCondition", "HealthCondition")
                        .WithMany()
                        .HasForeignKey("HealthConditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BPOR.Domain.Entities.AuroraParticipant", "Participant")
                        .WithMany("HealthConditions")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HealthCondition");

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("BPOR.Domain.Entities.ParticipantIdentifier", b =>
                {
                    b.HasOne("BPOR.Domain.Entities.RefData.IdentifierType", "Type")
                        .WithMany()
                        .HasForeignKey("IdentifierTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BPOR.Domain.Entities.AuroraParticipant", "Participant")
                        .WithMany("ParticipantIdentifiers")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Participant");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("BPOR.Domain.Entities.SourceReference", b =>
                {
                    b.HasOne("BPOR.Domain.Entities.AuroraParticipant", "Participant")
                        .WithMany("SourceReferences")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("BPOR.Domain.Entities.AuroraParticipant", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("HealthConditions");

                    b.Navigation("ParticipantIdentifiers");

                    b.Navigation("SourceReferences");
                });
#pragma warning restore 612, 618
        }
    }
}