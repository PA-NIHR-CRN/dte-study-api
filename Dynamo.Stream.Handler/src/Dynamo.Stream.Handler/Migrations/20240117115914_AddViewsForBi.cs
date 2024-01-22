using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    public partial class AddViewsForBi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // SQL command to create AnonymisedParticipantView
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

            // SQL command to create AnonymisedAddressView
            migrationBuilder.Sql(@"
                CREATE VIEW ParticipantAddress_Anonymised AS
                SELECT pa.Id,
                pa.Town,
                LEFT(TRIM(pa.Postcode), CHAR_LENGTH(TRIM(pa.Postcode)) - 3) AS Outcode, 
                pa.ParticipantId,
                pa.IsDeleted
                FROM ParticipantAddress pa;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS AnonymisedParticipantView;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS AnonymisedAddressView;");
        }
    }
}
