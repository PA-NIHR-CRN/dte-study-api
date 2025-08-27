using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class CRNCC2716AddPostcodetoParticipantAddress_Anonymised : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE VIEW ParticipantAddress_Anonymised AS
                SELECT pa.Id,
                pa.CanonicalTown AS Town,
                LEFT(TRIM(pa.Postcode), CHAR_LENGTH(TRIM(pa.Postcode)) - 3) AS Outcode, 
                pa.Postcode
                pa.ParticipantId,
                pa.IsDeleted
                FROM ParticipantAddress pa;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE VIEW ParticipantAddress_Anonymised AS
                SELECT pa.Id,
                pa.CanonicalTown AS Town,
                LEFT(TRIM(pa.Postcode), CHAR_LENGTH(TRIM(pa.Postcode)) - 3) AS Outcode, 
                pa.ParticipantId,
                pa.IsDeleted
                FROM ParticipantAddress pa;
            ");
        }
    }
}
