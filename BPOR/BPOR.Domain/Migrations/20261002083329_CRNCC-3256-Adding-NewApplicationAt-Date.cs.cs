using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPOR.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CRNCC3256AddingNewApplicationAtDatecs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NewApplicationAt",
                table: "Studies",
                type: "datetime(6)",
                nullable: true);
            
            migrationBuilder.Sql(@"
                UPDATE Studies
                SET NewApplicationAt = CreatedAt
                WHERE NewApplicationAt IS NULL
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewApplicationAt",
                table: "Studies");
        }
    }
}
