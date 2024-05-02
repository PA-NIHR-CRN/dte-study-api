using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmailRefData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailCampaignParticipant_EmailCampaigns_EmailCampaignId",
                table: "EmailCampaignParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailCampaignParticipant_Participants_ParticipantId",
                table: "EmailCampaignParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailCampaignParticipant_SysRefEmailDeliveryStatus_DeliveryS~",
                table: "EmailCampaignParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailCampaignParticipant",
                table: "EmailCampaignParticipant");

            migrationBuilder.RenameTable(
                name: "EmailCampaignParticipant",
                newName: "EmailCampaignParticipants");

            migrationBuilder.RenameIndex(
                name: "IX_EmailCampaignParticipant_ParticipantId",
                table: "EmailCampaignParticipants",
                newName: "IX_EmailCampaignParticipants_ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailCampaignParticipant_EmailCampaignId",
                table: "EmailCampaignParticipants",
                newName: "IX_EmailCampaignParticipants_EmailCampaignId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailCampaignParticipant_DeliveryStatusId",
                table: "EmailCampaignParticipants",
                newName: "IX_EmailCampaignParticipants_DeliveryStatusId");

            migrationBuilder.AlterColumn<double>(
                name: "SearchRadiusMiles",
                table: "FilterCriterias",
                type: "double",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailCampaignParticipants",
                table: "EmailCampaignParticipants",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "SysRefEmailDeliveryStatus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Pending", "Pending" });

            migrationBuilder.UpdateData(
                table: "SysRefEmailDeliveryStatus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sent", "Sent" });

            migrationBuilder.UpdateData(
                table: "SysRefEmailDeliveryStatus",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Delivered", "Delivered" });

            migrationBuilder.InsertData(
                table: "SysRefEmailDeliveryStatus",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[] { 4, "RegisteredInterest", "RegisteredInterest", false });

            migrationBuilder.AddForeignKey(
                name: "FK_EmailCampaignParticipants_EmailCampaigns_EmailCampaignId",
                table: "EmailCampaignParticipants",
                column: "EmailCampaignId",
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
                name: "FK_EmailCampaignParticipants_SysRefEmailDeliveryStatus_Delivery~",
                table: "EmailCampaignParticipants",
                column: "DeliveryStatusId",
                principalTable: "SysRefEmailDeliveryStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailCampaignParticipants_EmailCampaigns_EmailCampaignId",
                table: "EmailCampaignParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailCampaignParticipants_Participants_ParticipantId",
                table: "EmailCampaignParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailCampaignParticipants_SysRefEmailDeliveryStatus_Delivery~",
                table: "EmailCampaignParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailCampaignParticipants",
                table: "EmailCampaignParticipants");

            migrationBuilder.DeleteData(
                table: "SysRefEmailDeliveryStatus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.RenameTable(
                name: "EmailCampaignParticipants",
                newName: "EmailCampaignParticipant");

            migrationBuilder.RenameIndex(
                name: "IX_EmailCampaignParticipants_ParticipantId",
                table: "EmailCampaignParticipant",
                newName: "IX_EmailCampaignParticipant_ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailCampaignParticipants_EmailCampaignId",
                table: "EmailCampaignParticipant",
                newName: "IX_EmailCampaignParticipant_EmailCampaignId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailCampaignParticipants_DeliveryStatusId",
                table: "EmailCampaignParticipant",
                newName: "IX_EmailCampaignParticipant_DeliveryStatusId");

            migrationBuilder.AlterColumn<decimal>(
                name: "SearchRadiusMiles",
                table: "FilterCriterias",
                type: "decimal(65,30)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailCampaignParticipant",
                table: "EmailCampaignParticipant",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "SysRefEmailDeliveryStatus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Sent", "Sent" });

            migrationBuilder.UpdateData(
                table: "SysRefEmailDeliveryStatus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "Description" },
                values: new object[] { "Delivered", "Delivered" });

            migrationBuilder.UpdateData(
                table: "SysRefEmailDeliveryStatus",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Code", "Description" },
                values: new object[] { "RegisteredInterest", "RegisteredInterest" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmailCampaignParticipant_EmailCampaigns_EmailCampaignId",
                table: "EmailCampaignParticipant",
                column: "EmailCampaignId",
                principalTable: "EmailCampaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailCampaignParticipant_Participants_ParticipantId",
                table: "EmailCampaignParticipant",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailCampaignParticipant_SysRefEmailDeliveryStatus_DeliveryS~",
                table: "EmailCampaignParticipant",
                column: "DeliveryStatusId",
                principalTable: "SysRefEmailDeliveryStatus",
                principalColumn: "Id");
        }
    }
}
