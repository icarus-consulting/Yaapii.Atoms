using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Tests.Enumerable
{
    public sealed class ExactAmountTests
    {
        [Fact]
        public void DetectsMatch()
        {
            Assert.True(
                new ExactAmount(
                    3,
                    new ManyOf("a", "b", "c")
                ).Value()
            );
        }

        [Fact]
        public void NoMatchOnLess()
        {
            Assert.False(
                new ExactAmount(
                    3,
                    new ManyOf("a", "b")
                ).Value()
            );
        }

        [Fact]
        public void NoMatchOnMore()
        {
            Assert.False(
                new ExactAmount(
                    3,
                    new ManyOf("a", "b", "c", "d")
                ).Value()
            );
        }
    }
}
