using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class Offlinejourneytablerename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailCampaignParticipants_EmailCampaigns_CampaignId",
                table: "EmailCampaignParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailCampaignParticipants_Participants_ParticipantId",
                table: "EmailCampaignParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailCampaignParticipants_SysRefDeliveryStatus_DeliveryStatu~",
                table: "EmailCampaignParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailCampaigns_FilterCriterias_FilterCriteriaId",
                table: "EmailCampaigns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailCampaigns",
                table: "EmailCampaigns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailCampaignParticipants",
                table: "EmailCampaignParticipants");

            migrationBuilder.RenameTable(
                name: "EmailCampaigns",
                newName: "Campaigns");

            migrationBuilder.RenameTable(
                name: "EmailCampaignParticipants",
                newName: "CampaignParticipants");

            migrationBuilder.RenameIndex(
                name: "IX_EmailCampaigns_FilterCriteriaId",
                table: "Campaigns",
                newName: "IX_Campaigns_FilterCriteriaId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailCampaignParticipants_ParticipantId",
                table: "CampaignParticipants",
                newName: "IX_CampaignParticipants_ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailCampaignParticipants_DeliveryStatusId",
                table: "CampaignParticipants",
                newName: "IX_CampaignParticipants_DeliveryStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailCampaignParticipants_CampaignId",
                table: "CampaignParticipants",
                newName: "IX_CampaignParticipants_CampaignId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Campaigns",
                table: "Campaigns",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CampaignParticipants",
                table: "CampaignParticipants",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "EmailCampaigns");

            migrationBuilder.RenameTable(
                name: "CampaignParticipants",
                newName: "EmailCampaignParticipants");

            migrationBuilder.RenameIndex(
                name: "IX_Campaigns_FilterCriteriaId",
                table: "EmailCampaigns",
                newName: "IX_EmailCampaigns_FilterCriteriaId");

            migrationBuilder.RenameIndex(
                name: "IX_CampaignParticipants_ParticipantId",
                table: "EmailCampaignParticipants",
                newName: "IX_EmailCampaignParticipants_ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_CampaignParticipants_DeliveryStatusId",
                table: "EmailCampaignParticipants",
                newName: "IX_EmailCampaignParticipants_DeliveryStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_CampaignParticipants_CampaignId",
                table: "EmailCampaignParticipants",
                newName: "IX_EmailCampaignParticipants_CampaignId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailCampaigns",
                table: "EmailCampaigns",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailCampaignParticipants",
                table: "EmailCampaignParticipants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailCampaignParticipants_EmailCampaigns_CampaignId",
                table: "EmailCampaignParticipants",
                column: "CampaignId",
                principalTable: "EmailCampaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailCampaignParticipants_Participants_ParticipantId",
                table: "EmailCampaignParticipants",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailCampaignParticipants_SysRefDeliveryStatus_DeliveryStatu~",
                table: "EmailCampaignParticipants",
                column: "DeliveryStatusId",
                principalTable: "SysRefDeliveryStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailCampaigns_FilterCriterias_FilterCriteriaId",
                table: "EmailCampaigns",
                column: "FilterCriteriaId",
                principalTable: "FilterCriterias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
