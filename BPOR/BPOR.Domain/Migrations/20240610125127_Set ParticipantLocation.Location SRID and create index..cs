using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class SetParticipantLocationLocationSRIDandcreateindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "ParticipantLocation",
                type: "point",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "point")
                .Annotation("MySql:SpatialReferenceSystemId", 4326);

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantLocation_Location",
                table: "ParticipantLocation",
                column: "Location")
                .Annotation("MySql:SpatialIndex", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParticipantLocation_Location",
                table: "ParticipantLocation");

            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "ParticipantLocation",
                type: "point",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "point")
                .OldAnnotation("MySql:SpatialReferenceSystemId", 4326);
        }
    }
}
