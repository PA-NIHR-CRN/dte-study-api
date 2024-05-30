using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class FailedDeliveryStatusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SysRefEmailDeliveryStatus",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[] { 5, "Failed", "Failed", false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SysRefEmailDeliveryStatus",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
