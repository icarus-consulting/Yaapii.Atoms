using System;
using Xunit;

namespace Yaapii.Atoms.Error.Tests
{
    public sealed class FailNullTests
    {
        [Fact]
        public void FailsWhenNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new FailNull(null).Go()
            );
        }

        [Fact]
        public void ThrowsSpecificException()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
                new FailNull(
                    null,
                    new IndexOutOfRangeException()
                ).Go()
            );
        }
    }
}
