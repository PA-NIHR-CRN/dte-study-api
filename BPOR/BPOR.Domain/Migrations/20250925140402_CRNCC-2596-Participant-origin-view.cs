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
                    x.Id,
                    x.ParticipantId,
                    x.FirstName,
                    x.LastName,
                    x.IdentifierTypeId,
                    x.Description,
                    x.CreatedAt
                FROM (
                    SELECT 
                        pi.Id AS Id,
                        pi.ParticipantId AS ParticipantId,
                        p.FirstName AS FirstName,
                        p.LastName AS LastName,
                        pi.IdentifierTypeId AS IdentifierTypeId,
                        CASE
                            WHEN pi.IdentifierTypeId = 1 THEN 'BPOR'
                            WHEN pi.IdentifierTypeId = 2 THEN 'NHS'
                            ELSE it.Description
                        END AS Description,
                        p.CreatedAt AS CreatedAt,

                        ROW_NUMBER() OVER (
                            PARTITION BY pi.ParticipantId
                            ORDER BY pi.Id ASC
                        ) AS rn

                    FROM dte.ParticipantIdentifiers pi
                    JOIN dte.SysRefIdentifierType it 
                        ON pi.IdentifierTypeId = it.Id
                    JOIN dte.Participants p 
                        ON pi.ParticipantId = p.Id
                        
                    WHERE pi.Active = 1
                    AND p.Active = 1

                ) x
                WHERE x.rn = 1;
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS Participants_Origin;");
        }
    }
}
