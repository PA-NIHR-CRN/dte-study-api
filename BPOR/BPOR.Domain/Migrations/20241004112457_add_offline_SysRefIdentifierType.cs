using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class add_offline_SysRefIdentifierType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SysRefIdentifierType",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[] { 4, "Offline", "Offline", false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SysRefIdentifierType",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
