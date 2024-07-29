using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class CRNCC1957ReplaceFilterCriteriaDobwithAgeRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirthFrom",
                table: "FilterCriterias");

            migrationBuilder.DropColumn(
                name: "DateOfBirthTo",
                table: "FilterCriterias");

            migrationBuilder.AddColumn<int>(
                name: "AgeFrom",
                table: "FilterCriterias",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgeTo",
                table: "FilterCriterias",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeFrom",
                table: "FilterCriterias");

            migrationBuilder.DropColumn(
                name: "AgeTo",
                table: "FilterCriterias");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirthFrom",
                table: "FilterCriterias",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirthTo",
                table: "FilterCriterias",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
