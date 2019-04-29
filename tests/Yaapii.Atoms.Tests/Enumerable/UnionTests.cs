using System.Collections.Generic;
using Xunit;

namespace Yaapii.Atoms.Enumerable.Test
{
    public sealed class UnionTests
    {
        [Fact]
        public void Empty()
        {
            Assert.Empty(
                new Union<string>(
                    new EnumerableOf<string>("a", "b"),
                    new EnumerableOf<string>("c")
                )
            );
        }

        [Theory]
        [InlineData(new string[] { "a", "b" }, new string[] { "a", "b" }, new string[] { "a", "b" })]
        [InlineData(new string[] { "a", "b" }, new string[] { "a" }, new string[] { "a" })]
        public void MatchesString(IEnumerable<string> a, IEnumerable<string> b, IEnumerable<string> expected)
        {
            Assert.Equal(
                expected,
                new Union<string>(
                   a, b
                )
            );
        }

        [Theory]
        [InlineData(new int[] { 1, 2 }, new int[] { 1, 2 }, new int[] { 1, 2 })]
        [InlineData(new int[] { 1, 2 }, new int[] { 1 }, new int[] { 1 })]
        public void MatchesInt(IEnumerable<int> a, IEnumerable<int> b, IEnumerable<int> expected)
        {
            Assert.Equal(
                expected,
                new Union<int>(
                   a, b
                )
            );
        }
    }
}