using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    public partial class Add_Field_Stage2CompleteUtc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Stage2CompleteUtc",
                table: "Participants",
                type: "datetime(6)",
                nullable: true);
            
            migrationBuilder.Sql("DROP VIEW IF EXISTS Participants_Anonymised");

            
            migrationBuilder.Sql(@"
                CREATE VIEW Participants_Anonymised AS
                SELECT p.Id,
                       p.RegistrationConsent,
                       p.RegistrationConsentAtUtc,
                       p.CreatedAt,
                       p.UpdatedAt,
                       p.EthnicGroup,
                       p.DateOfBirth,
                       p.RemovalOfConsentRegistrationAtUtc,
                       p.HasLongTermCondition,
                       p.GenderIsSameAsSexRegisteredAtBirth,
                       p.IsDeleted,
                       p.DailyLifeImpactId,
                       p.CommunicationLanguageId,
                       p.GenderId,
                       p.Stage2CompleteUtc
                FROM Participants p
                GROUP BY p.Id;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stage2CompleteUtc",
                table: "Participants");
            
            migrationBuilder.Sql("DROP VIEW IF EXISTS Participants_Anonymised");
            
            migrationBuilder.Sql(@"
                CREATE VIEW Participants_Anonymised AS
                SELECT p.Id,
                       p.RegistrationConsent,
                       p.RegistrationConsentAtUtc,
                       p.CreatedAt,
                       p.UpdatedAt,
                       p.EthnicGroup,
                       p.DateOfBirth,
                       p.RemovalOfConsentRegistrationAtUtc,
                       p.HasLongTermCondition,
                       p.GenderIsSameAsSexRegisteredAtBirth,
                       p.IsDeleted,
                       p.DailyLifeImpactId,
                       p.CommunicationLanguageId,
                       p.GenderId
                FROM Participants p
                GROUP BY p.Id;
            ");
        }
    }
}
