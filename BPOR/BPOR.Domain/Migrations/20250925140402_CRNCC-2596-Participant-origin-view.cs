using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class CRNCC2596Participantoriginview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.Sql(@"
                CREATE VIEW Participants_Origin AS
                SELECT
                pi.Id,
                pi.ParticipantId,
                p.FirstName,
                p.LastName,
                pi.IdentifierTypeId,
                CASE 
                    WHEN pi.IdentifierTypeId = 1 THEN 'BPOR'
                    WHEN pi.IdentifierTypeId = 2 THEN 'NHS'
                    ELSE it.Description
                END AS Description,
                pi.CreatedAt
                FROM dte.ParticipantIdentifiers pi
                JOIN dte.SysRefIdentifierType it ON pi.IdentifierTypeId = it.Id
                JOIN dte.Participants p ON pi.ParticipantId = p.Id
                ORDER BY pi.ParticipantId, pi.IdentifierTypeId;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS Participants_Origin;");
        }
    }
}
