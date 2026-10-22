using Xunit;

namespace NIHR.SqlWriter.Tests;

public class SqlWriterTests
{
    [Fact]
    public void Select_WritesColumnsTableAndWhereClause()
    {
        using var output = new StringWriter();
        var writer = new MySqlSqlWriter(output);

        writer.Select("Studies",
            [
                new SqlIdentifier("Id"),
                new SqlIdentifier("StudyName")
            ],
            new ComparisonExpr
            {
                Lhs = new SqlIdentifier("Id"),
                Op = ComparisonOperator.Equals,
                Rhs = new SqlLiteral(42)
            });

        var newline = Environment.NewLine;
        Assert.Equal(
            $"SELECT{newline}    `Id`, `StudyName`{newline}FROM{newline}    `Studies`{newline}WHERE{newline}    `Id` = 42{newline};{newline}",
            output.ToString());
    }
}