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
                CREATE VIEW AnonymisedParticipantView AS
                SELECT p.Id,
                       p.RegistrationConsent                                                AS ConsentRegistration,
                       p.RegistrationConsentAtUtc                                     AS ConsentRegistrationAtUtc,
                       p.RemovalOfConsentRegistrationAtUtc,
                       p.DateOfBirth,
                       p.GenderIsSameAsSexRegisteredAtBirth                AS SexRegisteredAtBirth,
                       p.EthnicGroup,
                       p.HasLongTermCondition                                        AS Disability,
                       p.CreatedAt                                                              AS CreatedAtUtc,
                       p.UpdatedAt                                                             AS UpdatedAtUtc
                FROM Participants p
                GROUP BY p.Id;
            ");

            // SQL command to create AnonymisedAddressView
            migrationBuilder.Sql(@"
                CREATE VIEW AnonymisedAddressView AS
                SELECT LEFT(TRIM(pa.Postcode), CHAR_LENGTH(TRIM(pa.Postcode)) - 3) AS Outcode, pa.Town
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
