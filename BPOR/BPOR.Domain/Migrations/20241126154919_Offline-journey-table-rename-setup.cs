using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NIHR.CRN.CPMS.Database.Extensions;
using static NIHR.CRN.CPMS.Database.Extensions.MigrationBuilderExtensions;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class Offlinejourneytablerenamesetup : Migration
    {

        private const string ScriptIdentifier = "20241126161327_Offline-journey-table-rename-setup";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailCampaignParticipants_EmailCampaigns_EmailCampaignId",
                table: "EmailCampaignParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailCampaignParticipants_SysRefEmailDeliveryStatus_Delivery~",
                table: "EmailCampaignParticipants");

            migrationBuilder.DropTable(
                name: "SysRefEmailDeliveryStatus");

            migrationBuilder.DropIndex(
                name: "IX_EmailCampaignParticipants_EmailCampaignId",
                table: "EmailCampaignParticipants");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "EmailCampaignParticipants");

            migrationBuilder.RenameColumn(
                name: "EmailTemplateId",
                table: "EmailCampaigns",
                newName: "TemplateId");

            migrationBuilder.RenameColumn(
                name: "EmailCampaignId",
                table: "EmailCampaignParticipants",
                newName: "CampaignId");

            migrationBuilder.AddColumn<int>(
                name: "ContactMethodId",
                table: "FilterCriterias",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "EmailCampaigns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CampaignTypeId",
                table: "EmailCampaignParticipants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.SqlFromFile(ScriptIdentifier, MigrationDirection.Up);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailCampaignParticipants_CampaignTypeId_CampaignId",
                table: "EmailCampaignParticipants",
                column: "CampaignTypeId",
                principalTable: "SysRefContactMethod",
                principalColumn: "Id"
                );

            migrationBuilder.CreateTable(
                name: "FilterContactMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FilterCriteriaId = table.Column<int>(type: "int", nullable: false),
                    ContactMethodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterContactMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterContactMethod_SysRefContactMethod_ContactMethodId",
                        column: x => x.ContactMethodId,
                        principalTable: "SysRefContactMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SysRefDeliveryStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRefDeliveryStatus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "SysRefDeliveryStatus",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Pending", "Pending", false },
                    { 2, "Sent", "Sent", false },
                    { 3, "Delivered", "Delivered", false },
                    { 4, "RegisteredInterest", "RegisteredInterest", false },
                    { 5, "Failed", "Failed", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailCampaignParticipants_CampaignId",
                table: "EmailCampaignParticipants",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterContactMethod_ContactMethodId",
                table: "FilterContactMethod",
                column: "ContactMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailCampaignParticipants_EmailCampaigns_CampaignId",
                table: "EmailCampaignParticipants",
                column: "CampaignId",
                principalTable: "EmailCampaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailCampaignParticipants_SysRefDeliveryStatus_DeliveryStatu~",
                table: "EmailCampaignParticipants",
                column: "DeliveryStatusId",
                principalTable: "SysRefDeliveryStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailCampaignParticipants_EmailCampaigns_CampaignId",
                table: "EmailCampaignParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailCampaignParticipants_SysRefDeliveryStatus_DeliveryStatu~",
                table: "EmailCampaignParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailCampaignParticipants_CampaignTypeId_CampaignId",
                table: "EmailCampaignParticipants");

            migrationBuilder.DropTable(
                name: "FilterContactMethod");

            migrationBuilder.DropTable(
                name: "SysRefDeliveryStatus");

            migrationBuilder.DropIndex(
                name: "IX_EmailCampaignParticipants_CampaignId",
                table: "EmailCampaignParticipants");

            migrationBuilder.DropColumn(
                name: "ContactMethodId",
                table: "FilterCriterias");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "EmailCampaigns");

            migrationBuilder.RenameColumn(
                name: "CampaignId",
                table: "EmailCampaignParticipants",
                newName: "EmailCampaignId");

            migrationBuilder.RenameColumn(
                name: "TemplateId",
                table: "EmailCampaigns",
                newName: "EmailTemplateId");

            migrationBuilder.DropColumn(
                name: "CampaignTypeId",
                table: "EmailCampaignParticipants");

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "EmailCampaignParticipants",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SysRefEmailDeliveryStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRefEmailDeliveryStatus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "SysRefEmailDeliveryStatus",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Pending", "Pending", false },
                    { 2, "Sent", "Sent", false },
                    { 3, "Delivered", "Delivered", false },
                    { 4, "RegisteredInterest", "RegisteredInterest", false },
                    { 5, "Failed", "Failed", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailCampaignParticipants_EmailCampaignId",
                table: "EmailCampaignParticipants",
                column: "EmailCampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailCampaignParticipants_EmailCampaigns_EmailCampaignId",
                table: "EmailCampaignParticipants",
                column: "EmailCampaignId",
                principalTable: "EmailCampaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailCampaignParticipants_SysRefEmailDeliveryStatus_Delivery~",
                table: "EmailCampaignParticipants",
                column: "DeliveryStatusId",
                principalTable: "SysRefEmailDeliveryStatus",
                principalColumn: "Id");
        }
    }
}
