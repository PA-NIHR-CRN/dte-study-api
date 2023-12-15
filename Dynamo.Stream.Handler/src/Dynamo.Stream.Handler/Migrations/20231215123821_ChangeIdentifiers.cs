using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    public partial class ChangeIdentifiers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Value",
                table: "ParticipantIdentifiers",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantIdentifiers_Value",
                table: "ParticipantIdentifiers",
                column: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParticipantIdentifiers_Value",
                table: "ParticipantIdentifiers");

            migrationBuilder.DeleteData(
                table: "SysRefIdentifierType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Pk",
                table: "ParticipantIdentifiers");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ParticipantIdentifiers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
        }
    }
}
