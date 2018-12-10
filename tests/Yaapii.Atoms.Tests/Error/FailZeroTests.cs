using System;
using Xunit;

namespace Yaapii.Atoms.Error.Tests
{
    public sealed class FailZeroTests
    {
        [Fact]
        public void FailsWhenNumberIsZero()
        {
            Assert.Throws<Exception>(() =>
                new FailZero(0).Go()
            );
        }

        [Fact]
        public void ThrowsSpecificException()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
                new FailZero(
                    0,
                    new IndexOutOfRangeException()
                ).Go()
            );
        }
    }
}
