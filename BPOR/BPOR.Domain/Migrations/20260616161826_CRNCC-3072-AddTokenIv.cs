using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPOR.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CRNCC3072AddTokenIv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ParticipantIdentifiers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ParticipantIdentifiers");

            migrationBuilder.AddColumn<string>(
                name: "TokenIv",
                table: "CampaignParticipant",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenIv",
                table: "CampaignParticipant");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ParticipantIdentifiers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ParticipantIdentifiers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
