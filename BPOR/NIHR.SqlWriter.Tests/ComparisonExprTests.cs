using Xunit;

namespace NIHR.SqlWriter.Tests;

public class ComparisonExprTests
{
    [Theory]
    [InlineData(ComparisonOperator.Equals, "=")]
    [InlineData(ComparisonOperator.NotEquals, "<>")]
    [InlineData(ComparisonOperator.GreaterThan, ">")]
    [InlineData(ComparisonOperator.GreaterThanOrEquals, ">=")]
    [InlineData(ComparisonOperator.LessThan, "<")]
    [InlineData(ComparisonOperator.LessThanOrEquals, "<=")]
    [InlineData(ComparisonOperator.Like, "LIKE")]
    [InlineData(ComparisonOperator.In, "IN")]
    public void VisitComparisonExpr_WritesOperator(ComparisonOperator operation, string expectedOperator)
    {
        using var output = new StringWriter();
        var writer = new MySqlSqlWriter(output);
        var comparison = new ComparisonExpr
        {
            Lhs = new SqlIdentifier("StudyName"),
            Op = operation,
            Rhs = new SqlLiteral("trial")
        };

        comparison.Accept(writer);

        Assert.Equal($"`StudyName` {expectedOperator} \"trial\"", output.ToString());
    }

    [Fact]
    public void VisitComparisonExpr_ThrowsForUnsupportedOperator()
    {
        using var output = new StringWriter();
        var writer = new MySqlSqlWriter(output);
        var comparison = new ComparisonExpr
        {
            Lhs = new SqlIdentifier("Id"),
            Op = (ComparisonOperator)int.MaxValue,
            Rhs = new SqlLiteral(1)
        };

        Assert.Throws<NotImplementedException>(() => comparison.Accept(writer));
    }
}