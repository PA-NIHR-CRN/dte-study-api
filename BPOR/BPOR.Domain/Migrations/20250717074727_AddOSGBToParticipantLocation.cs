using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class AddOSGBToParticipantLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Easting",
                table: "ParticipantLocation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Northing",
                table: "ParticipantLocation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantLocation_Easting_Northing",
                table: "ParticipantLocation",
                columns: new[] { "Easting", "Northing" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParticipantLocation_Easting_Northing",
                table: "ParticipantLocation");

            migrationBuilder.DropColumn(
                name: "Easting",
                table: "ParticipantLocation");

            migrationBuilder.DropColumn(
                name: "Northing",
                table: "ParticipantLocation");
        }
    }
}
