using System;
using Xunit;

namespace Yaapii.Atoms.Error.Tests
{
    public sealed class FailNoNumberTests
    {
        [Fact]
        public void FailsWhenNoNumber()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new FailNoNumber("Not a number").Go()
            );
        }

        [Fact]
        public void ThrowsSpecificException()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
                new FailNoNumber(
                    "Not a number",
                    new IndexOutOfRangeException()
                ).Go()
            );
        }
    }
}
