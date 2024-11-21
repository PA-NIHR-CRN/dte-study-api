using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class CRNCC2394addinglettercampaignfunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampaignParticipants_Campaigns_EmailCampaignId",
                table: "CampaignParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignParticipants_SysRefEmailDeliveryStatus_DeliveryStatu~",
                table: "CampaignParticipants");

            migrationBuilder.DropTable(
                name: "SysRefEmailDeliveryStatus");

            migrationBuilder.RenameColumn(
                name: "EmailTemplateId",
                table: "Campaigns",
                newName: "TemplateId");

            migrationBuilder.RenameColumn(
                name: "EmailCampaignId",
                table: "CampaignParticipants",
                newName: "CampaignId");

            migrationBuilder.RenameIndex(
                name: "IX_CampaignParticipants_EmailCampaignId",
                table: "CampaignParticipants",
                newName: "IX_CampaignParticipants_CampaignId");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Campaigns",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_FilterContactMethod_ContactMethodId",
                table: "FilterContactMethod",
                column: "ContactMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignParticipants_Campaigns_CampaignId",
                table: "CampaignParticipants",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignParticipants_SysRefDeliveryStatus_DeliveryStatusId",
                table: "CampaignParticipants",
                column: "DeliveryStatusId",
                principalTable: "SysRefDeliveryStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampaignParticipants_Campaigns_CampaignId",
                table: "CampaignParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignParticipants_SysRefDeliveryStatus_DeliveryStatusId",
                table: "CampaignParticipants");

            migrationBuilder.DropTable(
                name: "FilterContactMethod");

            migrationBuilder.DropTable(
                name: "SysRefDeliveryStatus");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Campaigns");

            migrationBuilder.RenameColumn(
                name: "TemplateId",
                table: "Campaigns",
                newName: "EmailTemplateId");

            migrationBuilder.RenameColumn(
                name: "CampaignId",
                table: "CampaignParticipants",
                newName: "EmailCampaignId");

            migrationBuilder.RenameIndex(
                name: "IX_CampaignParticipants_CampaignId",
                table: "CampaignParticipants",
                newName: "IX_CampaignParticipants_EmailCampaignId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignParticipants_Campaigns_EmailCampaignId",
                table: "CampaignParticipants",
                column: "EmailCampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignParticipants_SysRefEmailDeliveryStatus_DeliveryStatu~",
                table: "CampaignParticipants",
                column: "DeliveryStatusId",
                principalTable: "SysRefEmailDeliveryStatus",
                principalColumn: "Id");
        }
    }
}
