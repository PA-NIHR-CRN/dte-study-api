using FluentAssertions.Collections;

namespace BPOR.Rms.Tests.Utilities
{
    public static class FluentAssertionExtensions
    {
        public static void OnlyContain<T>(this GenericCollectionAssertions<T> assertions, T[] expected, string because = "", params object[] becuaseArgs)
        {
            foreach(var item in assertions.Subject.Except(expected))
            {
                assertions.CurrentAssertionChain.BecauseOf(because, becuaseArgs).FailWith("Element should not exist in collection: {0}", item);
            }
            foreach (var item in expected.Except(assertions.Subject))
            {
                assertions.CurrentAssertionChain.BecauseOf(because, becuaseArgs).FailWith("Item not found in collection: {0}", item);
            }
        }
    }
}
