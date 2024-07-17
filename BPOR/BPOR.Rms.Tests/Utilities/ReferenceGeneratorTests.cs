using System.Text.RegularExpressions;
using BPOR.Rms.Utilities;
using BPOR.Rms.Utilities.Interfaces;
using LuhnNet;

namespace BPOR.Rms.Tests.Utilities;

public partial class ReferenceGeneratorTests
{
    private readonly IReferenceGenerator _generator = new ReferenceGenerator();
    private const int _expectedLength = 19;
    private static readonly Regex _digitsOnlyRegex = MyRegex();

    [Fact]
    public void GenerateReference_ShouldReturnStringOfCorrectLength()
    {
        // Act
        var reference = _generator.GenerateReference();

        // Assert
        Assert.Equal(_expectedLength, reference.Length);
    }

    [Fact]
    public void GenerateReference_ShouldContainOnlyDigits()
    {
        // Act
        var reference = _generator.GenerateReference();

        // Assert
        Assert.Matches(_digitsOnlyRegex, reference);
    }

    [Fact]
    public void GenerateReference_ShouldHaveValidLuhnCheckDigit()
    {
        var reference = _generator.GenerateReference();

        Assert.True(Luhn.IsValid(reference));
    }

    [Fact]
    public void GenerateReference_ShouldBeUnique()
    {
        // Arrange
        var references = new HashSet<string>();

        // Act
        for (var i = 0; i < 1000; i++)
        {
            var reference = _generator.GenerateReference();
            references.Add(reference);
        }

        // Assert
        Assert.Equal(1000, references.Count);
    }

    [GeneratedRegex(@"^(\d{4}[\s-]?){3}\d{4}$")]
    private static partial Regex MyRegex();
}
