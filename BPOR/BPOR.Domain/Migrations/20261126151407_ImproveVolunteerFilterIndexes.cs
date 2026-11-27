using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPOR.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ImproveVolunteerFilterIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Participants_FilterChunk",
                table: "Participants",
                columns: new[] { "IsDeleted", "Id", "Stage2CompleteUtc", "DateOfBirth" });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantLocation_FilterGeo",
                table: "ParticipantLocation",
                columns: new[] { "ParticipantId", "IsDeleted", "Easting", "Northing" });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantHealthCondition_FilterInterests",
                table: "ParticipantHealthCondition",
                columns: new[] { "ParticipantId", "IsDeleted", "HealthConditionId" });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantContactMethod_FilterContactMethod",
                table: "ParticipantContactMethod",
                columns: new[] { "ParticipantId", "IsDeleted", "ContactMethodId" });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantContactMethod_Method_Deleted_Participant",
                table: "ParticipantContactMethod",
                columns: new[] { "ContactMethodId", "IsDeleted", "ParticipantId" });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignParticipant_FilterContacted",
                table: "CampaignParticipant",
                columns: new[] { "ParticipantId", "IsDeleted", "CampaignId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Participants_FilterChunk",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantLocation_FilterGeo",
                table: "ParticipantLocation");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantHealthCondition_FilterInterests",
                table: "ParticipantHealthCondition");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantContactMethod_FilterContactMethod",
                table: "ParticipantContactMethod");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantContactMethod_Method_Deleted_Participant",
                table: "ParticipantContactMethod");

            migrationBuilder.DropIndex(
                name: "IX_CampaignParticipant_FilterContacted",
                table: "CampaignParticipant");
        }
    }
}
