using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class AddHasLongTermConditionToFilter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilterHasLongTermCondition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FilterCriteriaId = table.Column<int>(type: "int", nullable: false),
                    YesNoPreferNotToSay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterHasLongTermCondition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterHasLongTermCondition_FilterCriterias_FilterCriteriaId",
                        column: x => x.FilterCriteriaId,
                        principalTable: "FilterCriterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FilterHasLongTermCondition_FilterCriteriaId",
                table: "FilterHasLongTermCondition",
                column: "FilterCriteriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterHasLongTermCondition");
        }
    }
}
