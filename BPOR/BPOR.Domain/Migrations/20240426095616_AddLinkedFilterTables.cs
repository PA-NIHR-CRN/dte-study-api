using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class AddLinkedFilterTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilterCriterias_SysRefGender_GenderId",
                table: "FilterCriterias");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantHealthCondition_FilterCriterias_FilterCriteriaId",
                table: "ParticipantHealthCondition");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantHealthCondition_FilterCriteriaId",
                table: "ParticipantHealthCondition");

            migrationBuilder.DropIndex(
                name: "IX_FilterCriterias_GenderId",
                table: "FilterCriterias");

            migrationBuilder.DropColumn(
                name: "FilterCriteriaId",
                table: "ParticipantHealthCondition");

            migrationBuilder.DropColumn(
                name: "CompletedRegistration",
                table: "FilterCriterias");

            migrationBuilder.DropColumn(
                name: "EthnicGroup",
                table: "FilterCriterias");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "FilterCriterias");

            migrationBuilder.DropColumn(
                name: "PostcodeDistricts",
                table: "FilterCriterias");

            migrationBuilder.RenameColumn(
                name: "RegisteredInterest",
                table: "FilterCriterias",
                newName: "IncludeRegisteredInterest");

            migrationBuilder.RenameColumn(
                name: "Recruited",
                table: "FilterCriterias",
                newName: "IncludeRecruited");

            migrationBuilder.RenameColumn(
                name: "GenderIsSameAsSexRegisteredAtBirth",
                table: "FilterCriterias",
                newName: "IncludeContacted");

            migrationBuilder.RenameColumn(
                name: "Contacted",
                table: "FilterCriterias",
                newName: "IncludeCompletedRegistration");

            migrationBuilder.CreateTable(
                name: "FilterAreaOfInterest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FilterCriteriaId = table.Column<int>(type: "int", nullable: false),
                    HealthConditionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterAreaOfInterest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterAreaOfInterest_FilterCriterias_FilterCriteriaId",
                        column: x => x.FilterCriteriaId,
                        principalTable: "FilterCriterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterAreaOfInterest_SysRefHealthCondition_HealthConditionId",
                        column: x => x.HealthConditionId,
                        principalTable: "SysRefHealthCondition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FilterGender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FilterCriteriaId = table.Column<int>(type: "int", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterGender", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterGender_FilterCriterias_FilterCriteriaId",
                        column: x => x.FilterCriteriaId,
                        principalTable: "FilterCriterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterGender_SysRefGender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "SysRefGender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FilterPostcode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FilterCriteriaId = table.Column<int>(type: "int", nullable: false),
                    PostcodeFragment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterPostcode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterPostcode_FilterCriterias_FilterCriteriaId",
                        column: x => x.FilterCriteriaId,
                        principalTable: "FilterCriterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "SysRefEthnicGroup",
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
                    table.PrimaryKey("PK_SysRefEthnicGroup", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FilterEthnicGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FilterCriteriaId = table.Column<int>(type: "int", nullable: false),
                    EthnicGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterEthnicGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterEthnicGroup_FilterCriterias_FilterCriteriaId",
                        column: x => x.FilterCriteriaId,
                        principalTable: "FilterCriterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterEthnicGroup_SysRefEthnicGroup_EthnicGroupId",
                        column: x => x.EthnicGroupId,
                        principalTable: "SysRefEthnicGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "SysRefEthnicGroup",
                columns: new[] { "Id", "Code", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Asian", "Asian", false },
                    { 2, "Black", "Black", false },
                    { 3, "Mixed", "Mixed", false },
                    { 4, "White", "White", false },
                    { 5, "Other", "Other", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilterAreaOfInterest_FilterCriteriaId",
                table: "FilterAreaOfInterest",
                column: "FilterCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterAreaOfInterest_HealthConditionId",
                table: "FilterAreaOfInterest",
                column: "HealthConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterEthnicGroup_EthnicGroupId",
                table: "FilterEthnicGroup",
                column: "EthnicGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterEthnicGroup_FilterCriteriaId",
                table: "FilterEthnicGroup",
                column: "FilterCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterGender_FilterCriteriaId",
                table: "FilterGender",
                column: "FilterCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterGender_GenderId",
                table: "FilterGender",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterPostcode_FilterCriteriaId",
                table: "FilterPostcode",
                column: "FilterCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterSexRegisteredAtBirth_FilterCriteriaId",
                table: "FilterSexRegisteredAtBirth",
                column: "FilterCriteriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterAreaOfInterest");

            migrationBuilder.DropTable(
                name: "FilterEthnicGroup");

            migrationBuilder.DropTable(
                name: "FilterGender");

            migrationBuilder.DropTable(
                name: "FilterPostcode");

            migrationBuilder.DropTable(
                name: "FilterSexRegisteredAtBirth");

            migrationBuilder.DropTable(
                name: "SysRefEthnicGroup");

            migrationBuilder.RenameColumn(
                name: "IncludeRegisteredInterest",
                table: "FilterCriterias",
                newName: "RegisteredInterest");

            migrationBuilder.RenameColumn(
                name: "IncludeRecruited",
                table: "FilterCriterias",
                newName: "Recruited");

            migrationBuilder.RenameColumn(
                name: "IncludeContacted",
                table: "FilterCriterias",
                newName: "GenderIsSameAsSexRegisteredAtBirth");

            migrationBuilder.RenameColumn(
                name: "IncludeCompletedRegistration",
                table: "FilterCriterias",
                newName: "Contacted");

            migrationBuilder.AddColumn<int>(
                name: "FilterCriteriaId",
                table: "ParticipantHealthCondition",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CompletedRegistration",
                table: "FilterCriterias",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EthnicGroup",
                table: "FilterCriterias",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "FilterCriterias",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostcodeDistricts",
                table: "FilterCriterias",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantHealthCondition_FilterCriteriaId",
                table: "ParticipantHealthCondition",
                column: "FilterCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriterias_GenderId",
                table: "FilterCriterias",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilterCriterias_SysRefGender_GenderId",
                table: "FilterCriterias",
                column: "GenderId",
                principalTable: "SysRefGender",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantHealthCondition_FilterCriterias_FilterCriteriaId",
                table: "ParticipantHealthCondition",
                column: "FilterCriteriaId",
                principalTable: "FilterCriterias",
                principalColumn: "Id");
        }
    }
}
