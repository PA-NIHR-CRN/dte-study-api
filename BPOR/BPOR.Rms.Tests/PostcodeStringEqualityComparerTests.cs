using BPOR.Registration.Stream.Handler.Mappers;

namespace BPOR.Rms.Tests
{
    public class PostcodeStringEqualityComparerTests
    {
        [Fact]
        public void NullEqualsNull()
        {
            var comparer = new PostcodeStringEqualityComparer();
            Assert.True(comparer.Equals(null, null));
        }

        [Fact]
        public void NullEqualsEmptyString()
        {
            var comparer = new PostcodeStringEqualityComparer();
            Assert.True(comparer.Equals(null, string.Empty));
            Assert.True(comparer.Equals(string.Empty, null));
        }

        [Fact]
        public void NullEqualsWhitespace()
        {
            var comparer = new PostcodeStringEqualityComparer();
            Assert.True(comparer.Equals(null, "   "));
            Assert.True(comparer.Equals("  ", null));
            Assert.True(comparer.Equals("\t\r\n", null));
        }

        [Fact]
        public void IdenticalPostcodesAreEqual()
        {
            var comparer = new PostcodeStringEqualityComparer();
            Assert.True(comparer.Equals("BT1 9HT", "BT1 9HT"));
        }

        [Fact]
        public void CaseDifferingPostcodesAreEqual()
        {
            var comparer = new PostcodeStringEqualityComparer();
            Assert.True(comparer.Equals("BT1 9ht", "bt1 9HT"));
        }

        [Fact]
        public void WhitespaceDifferingPostcodesAreEqual()
        {
            var comparer = new PostcodeStringEqualityComparer();
            Assert.True(comparer.Equals("BT19HT", "BT1 9HT"));
            Assert.True(comparer.Equals("BT1 9HT", "BT19HT"));
            Assert.True(comparer.Equals("BT19HT", "BT19HT "));
            Assert.True(comparer.Equals("BT1 9HT", "B T19HT"));
            Assert.True(comparer.Equals("BT1 9HT", " BT19HT "));
        }

        [Fact]
        public void SignificantlyDifferingPostcodesAreUnequal()
        {
            var comparer = new PostcodeStringEqualityComparer();
            Assert.False(comparer.Equals("BT1 9HT", "BT2 9HT"));
            Assert.False(comparer.Equals("BT1 9HT", "BT1 9HY"));
            Assert.False(comparer.Equals("BT1 9HT", "BT29HT"));
            Assert.False(comparer.Equals("BBT19HT", "BT19HT"));
            Assert.False(comparer.Equals(null, "BT1 9HT"));
            Assert.False(comparer.Equals("BT1 9HT", null));
            Assert.False(comparer.Equals(string.Empty, "BT1 9HT"));
            Assert.False(comparer.Equals("BT1 9HT", string.Empty));
        }
    }
}
