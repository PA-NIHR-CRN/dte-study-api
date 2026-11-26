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
                name: "IX_Participants_FilterAge",
                table: "Participants",
                columns: new[] { "IsDeleted", "DateOfBirth", "Stage2CompleteUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_FilterRegistration",
                table: "Participants",
                columns: new[] { "IsDeleted", "Stage2CompleteUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantHealthCondition_FilterInterests",
                table: "ParticipantHealthCondition",
                columns: new[] { "ParticipantId", "IsDeleted", "HealthConditionId" });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantContactMethod_FilterContactMethod",
                table: "ParticipantContactMethod",
                columns: new[] { "ParticipantId", "IsDeleted", "ContactMethodId" });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignParticipant_FilterContacted",
                table: "CampaignParticipant",
                columns: new[] { "ParticipantId", "IsDeleted", "CampaignId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Participants_FilterAge",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_FilterRegistration",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantHealthCondition_FilterInterests",
                table: "ParticipantHealthCondition");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantContactMethod_FilterContactMethod",
                table: "ParticipantContactMethod");

            migrationBuilder.DropIndex(
                name: "IX_CampaignParticipant_FilterContacted",
                table: "CampaignParticipant");
        }
    }
}
