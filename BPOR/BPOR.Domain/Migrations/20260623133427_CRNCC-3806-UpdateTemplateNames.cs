using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPOR.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CRNCC3806UpdateTemplateNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SysRefStudyResearcherEmailOptions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Introduction email");

            migrationBuilder.UpdateData(
                table: "SysRefStudyResearcherEmailOptions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Next steps email with pre-screener");

            migrationBuilder.UpdateData(
                table: "SysRefStudyResearcherEmailOptions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Next steps email with NO pre-screener");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SysRefStudyResearcherEmailOptions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Introductory email");

            migrationBuilder.UpdateData(
                table: "SysRefStudyResearcherEmailOptions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Offer Pre-Screener");

            migrationBuilder.UpdateData(
                table: "SysRefStudyResearcherEmailOptions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Without Pre-Screener");
        }
    }
}
