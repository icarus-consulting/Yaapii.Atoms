using System;
using Xunit;

namespace Yaapii.Atoms.Error.Tests
{
    public sealed class FailWhenTests
    {
        [Fact]
        public void FailsWhenConditionTrue()
        {
            Assert.Throws<ArgumentException>(() =>
                new FailWhen(true).Go()
            );
        }

        [Fact]
        public void ThrowsSpecificException()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
                new FailWhen(
                    () => true,
                    new IndexOutOfRangeException()
                ).Go()
            );
        }
    }
}
