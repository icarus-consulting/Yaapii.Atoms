using System;
using Xunit;

namespace Yaapii.Atoms.Error.Tests
{
    public sealed class FailNullOrEmptyTests
    {
        [Fact]
        public void FailsWhenNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new FailNullOrEmpty(null).Go()
            );
        }

        [Fact]
        public void FailsWhenEmpty()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new FailNullOrEmpty(String.Empty).Go()
            );
        }

        [Fact]
        public void ThrowsSpecificException()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
                new FailNullOrEmpty(
                    String.Empty,
                    new IndexOutOfRangeException()
                ).Go()
            );
        }
    }
}
