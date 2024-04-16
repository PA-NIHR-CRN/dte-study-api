using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class AddFilterCriteria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FilterCriteriaId",
                table: "ParticipantHealthCondition",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FilterCriterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudyId = table.Column<int>(type: "int", nullable: true),
                    Contacted = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    RegisteredInterest = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    CompletedRegistration = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Recruited = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    PostcodeDistricts = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullPostcode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SearchRadiusMiles = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    RegistrationFromDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RegistrationToDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateOfBirthFrom = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateOfBirthTo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: true),
                    GenderIsSameAsSexRegisteredAtBirth = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    EthnicGroup = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterCriterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterCriterias_Studies_StudyId",
                        column: x => x.StudyId,
                        principalTable: "Studies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FilterCriterias_SysRefGender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "SysRefGender",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantHealthCondition_FilterCriteriaId",
                table: "ParticipantHealthCondition",
                column: "FilterCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriterias_GenderId",
                table: "FilterCriterias",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriterias_StudyId",
                table: "FilterCriterias",
                column: "StudyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantHealthCondition_FilterCriterias_FilterCriteriaId",
                table: "ParticipantHealthCondition",
                column: "FilterCriteriaId",
                principalTable: "FilterCriterias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantHealthCondition_FilterCriterias_FilterCriteriaId",
                table: "ParticipantHealthCondition");

            migrationBuilder.DropTable(
                name: "FilterCriterias");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantHealthCondition_FilterCriteriaId",
                table: "ParticipantHealthCondition");

            migrationBuilder.DropColumn(
                name: "FilterCriteriaId",
                table: "ParticipantHealthCondition");
        }
    }
}
