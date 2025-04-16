using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class ojtablenamechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampaignParticipants_Campaigns_CampaignId",
                table: "CampaignParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignParticipants_Participants_ParticipantId",
                table: "CampaignParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignParticipants_SysRefDeliveryStatus_DeliveryStatusId",
                table: "CampaignParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_FilterCriterias_FilterCriteriaId",
                table: "Campaigns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Campaigns",
                table: "Campaigns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CampaignParticipants",
                table: "CampaignParticipants");

            migrationBuilder.RenameTable(
                name: "Campaigns",
                newName: "Campaign");

            migrationBuilder.RenameTable(
                name: "CampaignParticipants",
                newName: "CampaignParticipant");

            migrationBuilder.RenameIndex(
                name: "IX_Campaigns_FilterCriteriaId",
                table: "Campaign",
                newName: "IX_Campaign_FilterCriteriaId");

            migrationBuilder.RenameIndex(
                name: "IX_CampaignParticipants_ParticipantId",
                table: "CampaignParticipant",
                newName: "IX_CampaignParticipant_ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_CampaignParticipants_DeliveryStatusId",
                table: "CampaignParticipant",
                newName: "IX_CampaignParticipant_DeliveryStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_CampaignParticipants_CampaignId",
                table: "CampaignParticipant",
                newName: "IX_CampaignParticipant_CampaignId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Campaign",
                table: "Campaign",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CampaignParticipant",
                table: "CampaignParticipant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaign_FilterCriterias_FilterCriteriaId",
                table: "Campaign",
                column: "FilterCriteriaId",
                principalTable: "FilterCriterias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignParticipant_Campaign_CampaignId",
                table: "CampaignParticipant",
                column: "CampaignId",
                principalTable: "Campaign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignParticipant_Participants_ParticipantId",
                table: "CampaignParticipant",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignParticipant_SysRefDeliveryStatus_DeliveryStatusId",
                table: "CampaignParticipant",
                column: "DeliveryStatusId",
                principalTable: "SysRefDeliveryStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaign_FilterCriterias_FilterCriteriaId",
                table: "Campaign");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignParticipant_Campaign_CampaignId",
                table: "CampaignParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignParticipant_Participants_ParticipantId",
                table: "CampaignParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignParticipant_SysRefDeliveryStatus_DeliveryStatusId",
                table: "CampaignParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CampaignParticipant",
                table: "CampaignParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Campaign",
                table: "Campaign");

            migrationBuilder.RenameTable(
                name: "CampaignParticipant",
                newName: "CampaignParticipants");

            migrationBuilder.RenameTable(
                name: "Campaign",
                newName: "Campaigns");

            migrationBuilder.RenameIndex(
                name: "IX_CampaignParticipant_ParticipantId",
                table: "CampaignParticipants",
                newName: "IX_CampaignParticipants_ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_CampaignParticipant_DeliveryStatusId",
                table: "CampaignParticipants",
                newName: "IX_CampaignParticipants_DeliveryStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_CampaignParticipant_CampaignId",
                table: "CampaignParticipants",
                newName: "IX_CampaignParticipants_CampaignId");

            migrationBuilder.RenameIndex(
                name: "IX_Campaign_FilterCriteriaId",
                table: "Campaigns",
                newName: "IX_Campaigns_FilterCriteriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CampaignParticipants",
                table: "CampaignParticipants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Campaigns",
                table: "Campaigns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignParticipants_Campaigns_CampaignId",
                table: "CampaignParticipants",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignParticipants_Participants_ParticipantId",
                table: "CampaignParticipants",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignParticipants_SysRefDeliveryStatus_DeliveryStatusId",
                table: "CampaignParticipants",
                column: "DeliveryStatusId",
                principalTable: "SysRefDeliveryStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_FilterCriterias_FilterCriteriaId",
                table: "Campaigns",
                column: "FilterCriteriaId",
                principalTable: "FilterCriterias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
