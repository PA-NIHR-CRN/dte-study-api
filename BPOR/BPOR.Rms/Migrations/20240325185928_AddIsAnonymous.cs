using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPOR.Rms.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAnonymous : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAnonymous",
                table: "Study",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CommunicationLanguage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationLanguage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyLifeImpact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyLifeImpact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthCondition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthCondition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentifierType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentifierType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuroraParticipant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    RegistrationConsent = table.Column<bool>(type: "INTEGER", nullable: false),
                    RegistrationConsentAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Stage2CompleteUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    EthnicBackground = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    EthnicGroup = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RemovalOfConsentRegistrationAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    HasLongTermCondition = table.Column<bool>(type: "INTEGER", nullable: true),
                    GenderIsSameAsSexRegisteredAtBirth = table.Column<bool>(type: "INTEGER", nullable: true),
                    MobileNumber = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    LandlineNumber = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    NHSNumber = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DailyLifeImpactId = table.Column<int>(type: "INTEGER", nullable: true),
                    CommunicationLanguageId = table.Column<int>(type: "INTEGER", nullable: true),
                    GenderId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuroraParticipant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuroraParticipant_CommunicationLanguage_CommunicationLanguageId",
                        column: x => x.CommunicationLanguageId,
                        principalTable: "CommunicationLanguage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuroraParticipant_DailyLifeImpact_DailyLifeImpactId",
                        column: x => x.DailyLifeImpactId,
                        principalTable: "DailyLifeImpact",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuroraParticipant_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuroraParticipantAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AddressLine1 = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    AddressLine2 = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    AddressLine3 = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    AddressLine4 = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Town = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Postcode = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ParticipantId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuroraParticipantAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuroraParticipantAddress_AuroraParticipant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "AuroraParticipant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantHealthCondition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParticipantId = table.Column<int>(type: "INTEGER", nullable: false),
                    HealthConditionId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantHealthCondition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantHealthCondition_AuroraParticipant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "AuroraParticipant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantHealthCondition_HealthCondition_HealthConditionId",
                        column: x => x.HealthConditionId,
                        principalTable: "HealthCondition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantIdentifier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParticipantId = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdentifierTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantIdentifier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantIdentifier_AuroraParticipant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "AuroraParticipant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantIdentifier_IdentifierType_IdentifierTypeId",
                        column: x => x.IdentifierTypeId,
                        principalTable: "IdentifierType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SourceReference",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Pk = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    ParticipantId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceReference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SourceReference_AuroraParticipant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "AuroraParticipant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuroraParticipant_CommunicationLanguageId",
                table: "AuroraParticipant",
                column: "CommunicationLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_AuroraParticipant_DailyLifeImpactId",
                table: "AuroraParticipant",
                column: "DailyLifeImpactId");

            migrationBuilder.CreateIndex(
                name: "IX_AuroraParticipant_GenderId",
                table: "AuroraParticipant",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AuroraParticipantAddress_ParticipantId",
                table: "AuroraParticipantAddress",
                column: "ParticipantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantHealthCondition_HealthConditionId",
                table: "ParticipantHealthCondition",
                column: "HealthConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantHealthCondition_ParticipantId",
                table: "ParticipantHealthCondition",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantIdentifier_IdentifierTypeId",
                table: "ParticipantIdentifier",
                column: "IdentifierTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantIdentifier_ParticipantId",
                table: "ParticipantIdentifier",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceReference_ParticipantId",
                table: "SourceReference",
                column: "ParticipantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuroraParticipantAddress");

            migrationBuilder.DropTable(
                name: "ParticipantHealthCondition");

            migrationBuilder.DropTable(
                name: "ParticipantIdentifier");

            migrationBuilder.DropTable(
                name: "SourceReference");

            migrationBuilder.DropTable(
                name: "HealthCondition");

            migrationBuilder.DropTable(
                name: "IdentifierType");

            migrationBuilder.DropTable(
                name: "AuroraParticipant");

            migrationBuilder.DropTable(
                name: "CommunicationLanguage");

            migrationBuilder.DropTable(
                name: "DailyLifeImpact");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropColumn(
                name: "IsAnonymous",
                table: "Study");
        }
    }
}
