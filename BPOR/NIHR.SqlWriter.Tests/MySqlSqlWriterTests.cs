using Xunit;

namespace NIHR.SqlWriter.Tests;

public class MySqlSqlWriterTests
{
    [Theory]
    [InlineData("plain text", "\"plain text\"")]
    [InlineData("double quote: \"", "\"double quote: \\\"\"")]
    [InlineData("single quote: '", "\"single quote: \\'\"")]
    [InlineData("line\nbreak", "\"line\\nbreak\"")]
    [InlineData("carriage\rreturn", "\"carriage\\rreturn\"")]
    [InlineData("tab\tcharacter", "\"tab\\tcharacter\"")]
    [InlineData("back\\slash", "\"back\\\\slash\"")]
    public void WriteStringLiteral_EscapesString(string value, string expectedLiteral)
    {
        using var output = new StringWriter();
        var writer = new MySqlSqlWriter(output);

        writer.Visit(new SqlLiteral(value));

        Assert.Equal(expectedLiteral, output.ToString());
    }
}