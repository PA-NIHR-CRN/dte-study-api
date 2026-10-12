using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BPOR.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CRNCC3217NewStudyRegistrationUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HasNihrFunding",
                table: "Studies",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);
            
            migrationBuilder.Sql("""
                                     UPDATE Studies
                                     SET HasNihrFunding =
                                         CASE
                                             WHEN HasNihrFunding = 1 THEN 1
                                             WHEN HasNihrFunding = 0 THEN 2
                                         END;
                                 """);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Studies",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "HasEthicsApproval",
                table: "Studies",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InclusionCriteria",
                table: "Studies",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MainContactRole",
                table: "Studies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SysRefNihrFundingStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRefNihrFundingStatus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "SysRefNihrFundingStatus",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Yes", "Yes", false },
                    { 2, "No", "No", false },
                    { 3, "No, but I have applied for NIHR funding", "No, but I have applied for NIHR funding", false }
                });

            migrationBuilder.InsertData(
                table: "SysRefStudyStatus",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[] { 0, "Draft Application", "Draft Application", false });

            migrationBuilder.CreateIndex(
                name: "IX_Studies_HasNihrFunding",
                table: "Studies",
                column: "HasNihrFunding");

            migrationBuilder.AddForeignKey(
                name: "FK_Studies_SysRefNihrFundingStatus_HasNihrFunding",
                table: "Studies",
                column: "HasNihrFunding",
                principalTable: "SysRefNihrFundingStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studies_SysRefNihrFundingStatus_HasNihrFunding",
                table: "Studies");

            migrationBuilder.DropTable(
                name: "SysRefNihrFundingStatus");

            migrationBuilder.DropIndex(
                name: "IX_Studies_HasNihrFunding",
                table: "Studies");

            migrationBuilder.DeleteData(
                table: "SysRefStudyStatus",
                keyColumn: "Id",
                keyValue: 0);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "HasEthicsApproval",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "InclusionCriteria",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "MainContactRole",
                table: "Studies");
            
            migrationBuilder.Sql("""
                                     UPDATE Studies
                                     SET HasNihrFunding =
                                         CASE
                                             WHEN HasNihrFunding = 1 THEN 1
                                             WHEN HasNihrFunding = 2 THEN 0
                                             WHEN HasNihrFunding = 3 THEN 0
                                         END;
                                 """);

            migrationBuilder.AlterColumn<bool>(
                name: "HasNihrFunding",
                table: "Studies",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
