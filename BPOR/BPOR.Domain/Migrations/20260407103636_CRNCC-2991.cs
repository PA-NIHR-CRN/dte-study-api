using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPOR.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CRNCC2991 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasMultipleResearchLocations",
                table: "Studies",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreScreenerUrl",
                table: "Studies",
                type: "varchar(2048)",
                maxLength: 2048,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "SinglePersonResponsibleForRecruiting",
                table: "Studies",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasMultipleResearchLocations",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "PreScreenerUrl",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "SinglePersonResponsibleForRecruiting",
                table: "Studies");
        }
    }
}
