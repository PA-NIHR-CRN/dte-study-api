using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class RenameTable_SexRegisteredAtBirth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterSexRegisteredAtBirth");

            migrationBuilder.CreateTable(
                name: "FilterSexSameAsRegisteredAtBirth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FilterCriteriaId = table.Column<int>(type: "int", nullable: false),
                    YesNoPreferNotToSay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterSexSameAsRegisteredAtBirth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterSexSameAsRegisteredAtBirth_FilterCriterias_FilterCrite~",
                        column: x => x.FilterCriteriaId,
                        principalTable: "FilterCriterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FilterSexSameAsRegisteredAtBirth_FilterCriteriaId",
                table: "FilterSexSameAsRegisteredAtBirth",
                column: "FilterCriteriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterSexSameAsRegisteredAtBirth");

            migrationBuilder.CreateTable(
                name: "FilterSexRegisteredAtBirth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FilterCriteriaId = table.Column<int>(type: "int", nullable: false),
                    YesNoPreferNotToSay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterSexRegisteredAtBirth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterSexRegisteredAtBirth_FilterCriterias_FilterCriteriaId",
                        column: x => x.FilterCriteriaId,
                        principalTable: "FilterCriterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FilterSexRegisteredAtBirth_FilterCriteriaId",
                table: "FilterSexRegisteredAtBirth",
                column: "FilterCriteriaId");
        }
    }
}
