using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class add_ParticipantContactMethod_table_and_ref_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysRefContactMethod",
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
                    table.PrimaryKey("PK_SysRefContactMethod", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ParticipantContactMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParticipantId = table.Column<int>(type: "int", nullable: false),
                    ContactMethodId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantContactMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantContactMethod_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantContactMethod_SysRefContactMethod_ContactMethodId",
                        column: x => x.ContactMethodId,
                        principalTable: "SysRefContactMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "SysRefContactMethod",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Email", "Email", false },
                    { 2, "Letter", "Letter", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantContactMethod_ContactMethodId",
                table: "ParticipantContactMethod",
                column: "ContactMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantContactMethod_ParticipantId",
                table: "ParticipantContactMethod",
                column: "ParticipantId",
                unique: true);

            migrationBuilder.Sql(@"INSERT INTO
                                dte.ParticipantContactMethod (ParticipantId, contactMethodId)
                                SELECT dte.Participants.Id, 1
                                FROM dte.Participants;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantContactMethod");

            migrationBuilder.DropTable(
                name: "SysRefContactMethod");
        }
    }
}
