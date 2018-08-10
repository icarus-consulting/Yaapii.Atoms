using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Error.Tests
{
    public sealed class FailNotNullTests
    {
        [Fact]
        public void FailsWhenNotNull()
        {
            Assert.Throws<ArgumentException>(() =>
                new FailNotNull("object :-P").Go()
            );
        }

        [Fact]
        public void ThrowsSpecificException()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
                new FailNotNull(
                    "object :-P",
                    new IndexOutOfRangeException()
                ).Go()
            );
        }
    }
}
