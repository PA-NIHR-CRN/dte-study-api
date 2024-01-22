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
                       p.RemovalOfConsentRegistrationAtUtc,
                       p.DateOfBirth,
                       p.GenderIsSameAsSexRegisteredAtBirth,
                       p.EthnicGroup,
                       p.HasLongTermCondition,
                       p.CreatedAt,
                       p.UpdatedAt,
                       p.IsDeleted,
                       p.CommunicationLanguageId,
                       p.GenderId,
                       p.DailyLifeImpactId
                FROM Participants p
                GROUP BY p.Id;
            ");

            // SQL command to create AnonymisedAddressView
            migrationBuilder.Sql(@"
                CREATE VIEW ParticipantAddress_Anonymised AS
                SELECT LEFT(TRIM(pa.Postcode), CHAR_LENGTH(TRIM(pa.Postcode)) - 3) AS Outcode, 
                pa.Town,
                pa.Id,
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
