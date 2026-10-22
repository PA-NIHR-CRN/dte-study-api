using Xunit;

namespace NIHR.SqlWriter.Tests;

public class SqlWriterStatementTests
{
    [Fact]
    public void Select_WithNoColumnsOrPredicate_WritesSelectAll()
    {
        using var output = new StringWriter();
        var writer = new MySqlSqlWriter(output);

        writer.Select("Studies");

        var newline = Environment.NewLine;
        Assert.Equal(
            $"SELECT{newline}    *{newline}FROM{newline}    `Studies`{newline};{newline}",
            output.ToString());
    }

    [Fact]
    public void Update_WritesAssignmentsAndPredicate()
    {
        using var output = new StringWriter();
        var writer = new MySqlSqlWriter(output);

        writer.Update("Studies", new { StudyName = "Updated" }, new ComparisonExpr
        {
            Lhs = new SqlIdentifier("Id"),
            Op = ComparisonOperator.Equals,
            Rhs = new SqlLiteral(7)
        });

        var newline = Environment.NewLine;
        Assert.Equal(
            $"UPDATE `Studies` SET{newline}    `StudyName` = \"Updated\"{newline}WHERE{newline}    `Id` = 7{newline};{newline}",
            output.ToString());
    }


    [Fact]
    public void InsertBatch_RejectsNonPositiveMaximumBatchSize()
    {
        using var output = new StringWriter();
        var writer = new SmallBatchMySqlWriter(output, 0);

        var exception = Assert.Throws<InvalidOperationException>(() =>
            writer.Insert("Studies", new { Id = 1 }));

        Assert.Equal("MaxInsertBatchSize must be greater than zero.", exception.Message);
    }

    [Theory]
    [InlineData(LogicalBinOp.And, "AND")]
    [InlineData(LogicalBinOp.Or, "OR")]
    public void LogicPredicate_WritesLogicalOperator(LogicalBinOp operation, string expectedOperator)
    {
        using var output = new StringWriter();
        var writer = new MySqlSqlWriter(output);
        var predicate = new LogicBinOpExpr
        {
            Lhs = Comparison("FirstId", 1),
            Op = operation,
            Rhs = Comparison("SecondId", 2)
        };

        predicate.Accept(writer);

        Assert.Equal($"(`FirstId` = 1 {expectedOperator} `SecondId` = 2)",
            output.ToString());
    }

    private static ComparisonExpr Comparison(string column, int value) => new()
    {
        Lhs = new SqlIdentifier(column),
        Op = ComparisonOperator.Equals,
        Rhs = new SqlLiteral(value)
    };

    private sealed class SmallBatchMySqlWriter(TextWriter writer, int maxBatchSize) : MySqlSqlWriter(writer)
    {
        protected override int MaxInsertBatchSize => maxBatchSize;
    }
}