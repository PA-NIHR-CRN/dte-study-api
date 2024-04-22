using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class AddParticipantLocationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "ParticipantAddress");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "ParticipantAddress");

            migrationBuilder.CreateTable(
                name: "ParticipantLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Location = table.Column<Point>(type: "point", nullable: false),
                    IsApproximate = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ParticipantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantLocation_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantAddress_Postcode",
                table: "ParticipantAddress",
                column: "Postcode");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantLocation_ParticipantId",
                table: "ParticipantLocation",
                column: "ParticipantId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantLocation");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantAddress_Postcode",
                table: "ParticipantAddress");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "ParticipantAddress",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "ParticipantAddress",
                type: "double",
                nullable: true);
        }
    }
}
