using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BPOR.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CRNCC3072StudyResearcherEmailtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysRefStudyResearcherEmailOptions",
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
                    table.PrimaryKey("PK_SysRefStudyResearcherEmailOptions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudyResearcherEmails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudyResearcherId = table.Column<int>(type: "int", nullable: false),
                    StudyResearcherEmailOptionId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyResearcherEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyResearcherEmails_StudyResearcher_StudyResearcherId",
                        column: x => x.StudyResearcherId,
                        principalTable: "StudyResearcher",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudyResearcherEmails_SysRefStudyResearcherEmailOptions_Stud~",
                        column: x => x.StudyResearcherEmailOptionId,
                        principalTable: "SysRefStudyResearcherEmailOptions",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "SysRefStudyResearcherEmailOptions",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Introductory Email", "Introductory email", false },
                    { 2, "Offer Pre-Screener", "Offer Pre-Screener", false },
                    { 3, "Without Pre-Screener", "Without Pre-Screener", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudyResearcherEmails_StudyResearcherEmailOptionId",
                table: "StudyResearcherEmails",
                column: "StudyResearcherEmailOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyResearcherEmails_StudyResearcherId",
                table: "StudyResearcherEmails",
                column: "StudyResearcherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyResearcherEmails");

            migrationBuilder.DropTable(
                name: "SysRefStudyResearcherEmailOptions");
            
        }
    }
}
