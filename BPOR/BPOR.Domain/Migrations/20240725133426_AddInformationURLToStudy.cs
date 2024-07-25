using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class AddInformationURLToStudy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InformationUrl",
                table: "Studies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InformationUrl",
                table: "Studies");
        }
    }
}
