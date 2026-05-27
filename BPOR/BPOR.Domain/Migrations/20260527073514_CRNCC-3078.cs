using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPOR.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CRNCC3078 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VolunteerStudyInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudyId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StudyTypeId = table.Column<long>(type: "bigint", nullable: true),
                    WhatYouWillDo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CostReimbursement = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    HasIncentive = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    IncentiveDetails = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumberOfVisits = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StudyDuration = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StudyFormat = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OtherDetails = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExternalWebsiteUrl = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InfoToRegisterByEmail = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StagedPreScreenerUrl = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerStudyInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerStudyInformation_Studies_StudyId",
                        column: x => x.StudyId,
                        principalTable: "Studies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VolunteerStudyInformationContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VolunteerStudyInformationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Organisation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerStudyInformationContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerStudyInformationContact_VolunteerStudyInformation_V~",
                        column: x => x.VolunteerStudyInformationId,
                        principalTable: "VolunteerStudyInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VolunteerStudyInformationGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VolunteerStudyInformationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerStudyInformationGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerStudyInformationGroup_VolunteerStudyInformation_Vol~",
                        column: x => x.VolunteerStudyInformationId,
                        principalTable: "VolunteerStudyInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VolunteerStudyInformationSite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VolunteerStudyInformationId = table.Column<int>(type: "int", nullable: false),
                    AddressLine1 = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressLine2 = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressLine3 = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressLine4 = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressLine5 = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Postcode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerStudyInformationSite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerStudyInformationSite_VolunteerStudyInformation_Volu~",
                        column: x => x.VolunteerStudyInformationId,
                        principalTable: "VolunteerStudyInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VolunteerStudyInformationGroupCriteria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VolunteerStudyInformationGroupId = table.Column<int>(type: "int", nullable: false),
                    Criteria = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerStudyInformationGroupCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerStudyInformationGroupCriteria_VolunteerStudyInforma~",
                        column: x => x.VolunteerStudyInformationGroupId,
                        principalTable: "VolunteerStudyInformationGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerStudyInformation_StudyId",
                table: "VolunteerStudyInformation",
                column: "StudyId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerStudyInformationContact_VolunteerStudyInformationId",
                table: "VolunteerStudyInformationContact",
                column: "VolunteerStudyInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerStudyInformationGroup_VolunteerStudyInformationId",
                table: "VolunteerStudyInformationGroup",
                column: "VolunteerStudyInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerStudyInformationGroupCriteria_VolunteerStudyInforma~",
                table: "VolunteerStudyInformationGroupCriteria",
                column: "VolunteerStudyInformationGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerStudyInformationSite_VolunteerStudyInformationId",
                table: "VolunteerStudyInformationSite",
                column: "VolunteerStudyInformationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VolunteerStudyInformationContact");

            migrationBuilder.DropTable(
                name: "VolunteerStudyInformationGroupCriteria");

            migrationBuilder.DropTable(
                name: "VolunteerStudyInformationSite");

            migrationBuilder.DropTable(
                name: "VolunteerStudyInformationGroup");

            migrationBuilder.DropTable(
                name: "VolunteerStudyInformation");
        }
    }
}
