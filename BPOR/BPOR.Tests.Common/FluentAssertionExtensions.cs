using FluentAssertions.Collections;
using FluentAssertions.Execution;

namespace BPOR.Tests.Common
{
    public static class FluentAssertionExtensions
    {
        public static void OnlyContain<T>(this GenericCollectionAssertions<T> assertions, T[] expected, string because = "", params object[] becuaseArgs)
        {
            foreach (var item in assertions.Subject.Except(expected))
            {
                Execute.Assertion.BecauseOf(because, becuaseArgs).FailWith("Element should not exist in collection: {0}", item);
            }
            foreach (var item in expected.Except(assertions.Subject))
            {
                Execute.Assertion.BecauseOf(because, becuaseArgs).FailWith("Item not found in collection: {0}", item);
            }
        }
    }
}