using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BPOR.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CRNCC3215SudyStatusUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasVip",
                table: "Studies");

            migrationBuilder.AlterColumn<string>(
                name: "StudyName",
                table: "Studies",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Studies",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Studies",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ChiefInvestigatorEmail",
                table: "Studies",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "StudyStatusId",
                table: "Studies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SysRefRejectedReason",
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
                    table.PrimaryKey("PK_SysRefRejectedReason", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SysRefStudyStatus",
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
                    table.PrimaryKey("PK_SysRefStudyStatus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SysRefWithdrawnReason",
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
                    table.PrimaryKey("PK_SysRefWithdrawnReason", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudyStatusHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudyId = table.Column<int>(type: "int", nullable: false),
                    StudyStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyStatusHistory_Studies_StudyId",
                        column: x => x.StudyId,
                        principalTable: "Studies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyStatusHistory_SysRefStudyStatus_StudyStatusId",
                        column: x => x.StudyStatusId,
                        principalTable: "SysRefStudyStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudyStatusReasonHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AdditionalReasonText = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StudyStatusHistoryId = table.Column<int>(type: "int", nullable: false),
                    WithdrawnReasonId = table.Column<int>(type: "int", nullable: true),
                    RejectedReasonId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyStatusReasonHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyStatusReasonHistory_StudyStatusHistory_StudyStatusHisto~",
                        column: x => x.StudyStatusHistoryId,
                        principalTable: "StudyStatusHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyStatusReasonHistory_SysRefRejectedReason_RejectedReason~",
                        column: x => x.RejectedReasonId,
                        principalTable: "SysRefRejectedReason",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudyStatusReasonHistory_SysRefWithdrawnReason_WithdrawnReas~",
                        column: x => x.WithdrawnReasonId,
                        principalTable: "SysRefWithdrawnReason",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "SysRefRejectedReason",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Not NIHR-affiliated", "Not NIHR-affiliated", false },
                    { 2, "PPIE opportunity", "PPIE opportunity", false },
                    { 3, "Study already listed here", "Study already listed here", false },
                    { 4, "Not possible to recruit target population", "Not possible to recruit target population", false },
                    { 5, "Recruitment window too short", "Recruitment window too short", false },
                    { 6, "Misc", "Misc", false }
                });

            migrationBuilder.InsertData(
                table: "SysRefStudyStatus",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "New Application", "New Application", false },
                    { 2, "In Progress", "In Progress", false },
                    { 3, "Active", "Active", false },
                    { 4, "Concluded Successfully", "Concluded Successfully", false },
                    { 5, "Rejected", "Rejected", false },
                    { 6, "Withdrawn", "Withdrawn", false }
                });

            migrationBuilder.InsertData(
                table: "SysRefWithdrawnReason",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "No response from study team", "No response from study team", false },
                    { 2, "Study does not need additional support", "Study does not need additional support", false },
                    { 3, "Problems with Study", "Problems with Study", false },
                    { 4, "Study team has limited capacity", "Study team has limited capacity", false },
                    { 5, "Contact dropped by BPoR team", "Contact dropped by BPoR team", false },
                    { 6, "Other", "Other", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Studies_StudyStatusId",
                table: "Studies",
                column: "StudyStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyStatusHistory_StudyId",
                table: "StudyStatusHistory",
                column: "StudyId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyStatusHistory_StudyStatusId",
                table: "StudyStatusHistory",
                column: "StudyStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyStatusReasonHistory_RejectedReasonId",
                table: "StudyStatusReasonHistory",
                column: "RejectedReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyStatusReasonHistory_StudyStatusHistoryId",
                table: "StudyStatusReasonHistory",
                column: "StudyStatusHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyStatusReasonHistory_WithdrawnReasonId",
                table: "StudyStatusReasonHistory",
                column: "WithdrawnReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Studies_SysRefStudyStatus_StudyStatusId",
                table: "Studies",
                column: "StudyStatusId",
                principalTable: "SysRefStudyStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studies_SysRefStudyStatus_StudyStatusId",
                table: "Studies");

            migrationBuilder.DropTable(
                name: "StudyStatusReasonHistory");

            migrationBuilder.DropTable(
                name: "StudyStatusHistory");

            migrationBuilder.DropTable(
                name: "SysRefRejectedReason");

            migrationBuilder.DropTable(
                name: "SysRefWithdrawnReason");

            migrationBuilder.DropTable(
                name: "SysRefStudyStatus");

            migrationBuilder.DropIndex(
                name: "IX_Studies_StudyStatusId",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "ChiefInvestigatorEmail",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "StudyStatusId",
                table: "Studies");

            migrationBuilder.UpdateData(
                table: "Studies",
                keyColumn: "StudyName",
                keyValue: null,
                column: "StudyName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "StudyName",
                table: "Studies",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Studies",
                keyColumn: "FullName",
                keyValue: null,
                column: "FullName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Studies",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Studies",
                keyColumn: "EmailAddress",
                keyValue: null,
                column: "EmailAddress",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Studies",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "HasVip",
                table: "Studies",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
