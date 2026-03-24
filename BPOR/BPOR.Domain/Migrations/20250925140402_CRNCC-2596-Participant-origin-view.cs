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
                CREATE 
                    ALGORITHM = UNDEFINED 
                    DEFINER = `dte-stream-s`@`%` 
                    SQL SECURITY DEFINER
                VIEW `dte`.`Participants_Origin` AS
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
                    p.CreatedAt
                FROM dte.ParticipantIdentifiers pi

                JOIN (
                    SELECT ParticipantId, MIN(Id) AS MinId
                    FROM dte.ParticipantIdentifiers
                    WHERE IsDeleted = 0
                    GROUP BY ParticipantId
                ) first_pi
                    ON pi.ParticipantId = first_pi.ParticipantId
                AND pi.Id = first_pi.MinId

                JOIN dte.Participants p 
                    ON pi.ParticipantId = p.Id

                JOIN dte.SysRefIdentifierType it 
                    ON pi.IdentifierTypeId = it.Id

                WHERE pi.IsDeleted = 0
                AND p.IsDeleted = 0;
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS Participants_Origin;");
        }
    }
}
