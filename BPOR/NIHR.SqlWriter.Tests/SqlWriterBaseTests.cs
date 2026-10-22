using Xunit;

namespace NIHR.SqlWriter.Tests;

public class SqlWriterBaseTests
{
    [Fact]
    public void VisitSqlLiteral_WritesNull()
    {
        AssertLiteral(null, "NULL");
    }

    [Fact]
    public void VisitSqlLiteral_WritesEnumValueAndName()
    {
        AssertLiteral(TestStatus.Active, "1 /* Active */ ");
    }

    [Fact]
    public void VisitSqlLiteral_WritesString()
    {
        AssertLiteral("text", "\"text\"");
    }

    [Theory]
    [InlineData(true, "1")]
    [InlineData(false, "0")]
    public void VisitSqlLiteral_WritesBooleanAsInteger(bool value, string expected)
    {
        AssertLiteral(value, expected);
    }

    [Fact]
    public void VisitSqlLiteral_WritesDateTimeInSqlFormat()
    {
        AssertLiteral(new DateTime(2026, 10, 22, 16, 2, 58), "\"2026-10-22T16:02:58\"");
    }

    [Fact]
    public void VisitSqlLiteral_WritesInteger()
    {
        AssertLiteral(42, "42");
    }

    [Fact]
    public void VisitSqlLiteral_WritesLong()
    {
        AssertLiteral(9223372036854775807L, "9223372036854775807");
    }

    [Fact]
    public void VisitSqlLiteral_WritesGuidWithoutSeparators()
    {
        AssertLiteral(Guid.Parse("00112233-4455-6677-8899-aabbccddeeff"), "\"00112233445566778899aabbccddeeff\"");
    }

    [Fact]
    public void VisitSqlLiteral_ThrowsForUnsupportedType()
    {
        using var output = new StringWriter();
        var writer = new MySqlSqlWriter(output);

        var exception = Assert.Throws<InvalidOperationException>(() => writer.Visit(new SqlLiteral(1.5m)));

        Assert.Equal("Unsupported literal type Decimal", exception.Message);
    }

    private static void AssertLiteral(object? value, string expected)
    {
        using var output = new StringWriter();
        var writer = new MySqlSqlWriter(output);

        writer.Visit(new SqlLiteral(value));

        Assert.Equal(expected, output.ToString());
    }

    private enum TestStatus
    {
        Active = 1
    }
}