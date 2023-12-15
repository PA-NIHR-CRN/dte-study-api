using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    public partial class changeidentifiers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pk",
                table: "ParticipantIdentifiers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "SysRefIdentifierType",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[] { 3, "Deleted", "Deleted", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SysRefIdentifierType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Pk",
                table: "ParticipantIdentifiers");
        }
    }
}
