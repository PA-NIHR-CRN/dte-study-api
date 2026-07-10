using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPOR.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CRNCC3116 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasVip",
                table: "Studies",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "VipEmailLinkClickedAtUtc",
                table: "CampaignParticipant",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VipExternalLinkClickedAtUtc",
                table: "CampaignParticipant",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VipPrescreenerLinkClickedAtUtc",
                table: "CampaignParticipant",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasVip",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "VipEmailLinkClickedAtUtc",
                table: "CampaignParticipant");

            migrationBuilder.DropColumn(
                name: "VipExternalLinkClickedAtUtc",
                table: "CampaignParticipant");

            migrationBuilder.DropColumn(
                name: "VipPrescreenerLinkClickedAtUtc",
                table: "CampaignParticipant");
        }
    }
}
