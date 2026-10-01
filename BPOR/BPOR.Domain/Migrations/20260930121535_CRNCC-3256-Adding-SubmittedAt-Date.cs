using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPOR.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CRNCC3256AddingSubmittedAtDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedAt",
                table: "Studies",
                type: "datetime(6)",
                nullable: true);
            
            migrationBuilder.Sql(@"
                UPDATE Studies
                SET SubmittedAt = CreatedAt
                WHERE SubmittedAt IS NULL
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmittedAt",
                table: "Studies");
        }
    }
}
