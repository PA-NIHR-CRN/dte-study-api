using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalStudyColumnsForResearcher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AlreadyOpenToRecruitment",
                table: "Studies",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChiefInvestigator",
                table: "Studies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FundingCode",
                table: "Studies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "HasNihrFunding",
                table: "Studies",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParticipantsRecruited",
                table: "Studies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecruitmentEndDate",
                table: "Studies",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecruitmentStartDate",
                table: "Studies",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecruitmentTarget",
                table: "Studies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sponsors",
                table: "Studies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "SubmissionOutcomeId",
                table: "Studies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmittedId",
                table: "Studies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetPopulation",
                table: "Studies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SysRefSubmissionOutcome",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRefSubmissionOutcome", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SysRefSubmitted",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRefSubmitted", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "SysRefSubmissionOutcome",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Eligible for inclusion", "Eligible for inclusion", false },
                    { 2, "Awaiting outcome", "Awaiting outcome", false },
                    { 3, "Ineligible for inclusion", "Ineligible for inclusion", false }
                });

            migrationBuilder.InsertData(
                table: "SysRefSubmitted",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Yes", "Yes", false },
                    { 2, "No", "No", false },
                    { 3, "Not yet, but will be submitted", "Not yet, but will be submitted", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Studies_SubmissionOutcomeId",
                table: "Studies",
                column: "SubmissionOutcomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Studies_SubmittedId",
                table: "Studies",
                column: "SubmittedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Studies_SysRefSubmissionOutcome_SubmissionOutcomeId",
                table: "Studies",
                column: "SubmissionOutcomeId",
                principalTable: "SysRefSubmissionOutcome",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Studies_SysRefSubmitted_SubmittedId",
                table: "Studies",
                column: "SubmittedId",
                principalTable: "SysRefSubmitted",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studies_SysRefSubmissionOutcome_SubmissionOutcomeId",
                table: "Studies");

            migrationBuilder.DropForeignKey(
                name: "FK_Studies_SysRefSubmitted_SubmittedId",
                table: "Studies");

            migrationBuilder.DropTable(
                name: "SysRefSubmissionOutcome");

            migrationBuilder.DropTable(
                name: "SysRefSubmitted");

            migrationBuilder.DropIndex(
                name: "IX_Studies_SubmissionOutcomeId",
                table: "Studies");

            migrationBuilder.DropIndex(
                name: "IX_Studies_SubmittedId",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "AlreadyOpenToRecruitment",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "ChiefInvestigator",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "FundingCode",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "HasNihrFunding",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "ParticipantsRecruited",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "RecruitmentEndDate",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "RecruitmentStartDate",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "RecruitmentTarget",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "Sponsors",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "SubmissionOutcomeId",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "SubmittedId",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "TargetPopulation",
                table: "Studies");
        }
    }
}
