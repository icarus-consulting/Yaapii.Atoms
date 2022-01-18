using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Tests.Enumerable
{
    public sealed class LessThanTests
    {
        [Fact]
        public void DetectsLess()
        {
            Assert.True(
                new LessThan(
                    3,
                    new ManyOf("a", "b")
                ).Value()
            );
        }

        [Fact]
        public void NoMatchOnMore()
        {
            Assert.False(
                new LessThan(
                    3,
                    new ManyOf("a", "b", "c", "d")
                ).Value()
            );
        }

        [Fact]
        public void NoMatchOnEqual()
        {
            Assert.False(
                new LessThan(
                    3,
                    new ManyOf("a", "b", "c")
                ).Value()
            );
        }
    }
}
