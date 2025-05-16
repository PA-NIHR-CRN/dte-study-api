using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dynamo.Stream.Handler.Migrations
{
    /// <inheritdoc />
    public partial class CRNCC2479CanonicalTowndata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CanonicalTown",
                table: "ParticipantAddress",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE VIEW ParticipantAddress_Anonymised AS
                SELECT pa.Id,
                pa.Town,
                LEFT(TRIM(pa.Postcode), CHAR_LENGTH(TRIM(pa.Postcode)) - 3) AS Outcode, 
                pa.ParticipantId,
                pa.IsDeleted
                FROM ParticipantAddress pa;
            ");

            migrationBuilder.DropColumn(
                name: "CanonicalTown",
                table: "ParticipantAddress");
        }
    }
}
