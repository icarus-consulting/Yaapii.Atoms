using System;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Error.Tests
{
    public sealed class FailEmptyTests
    {
        [Fact]
        public void FailsWhenEmpty()
        {
            Assert.Throws<Exception>(() =>
                new FailEmpty<int>(new EnumerableOf<int>()).Go()
            );
        }

        [Fact]
        public void ThrowsSpecificException()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
                new FailEmpty<int>(
                    new EnumerableOf<int>(),
                    new IndexOutOfRangeException()
                ).Go()
            );
        }
    }
}
