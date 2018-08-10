using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Error;
using Yaapii.Atoms;

namespace Yaapii.Atoms.Error.Tests
{
    public sealed class FailEmptyOptTests
    {
        [Fact]
        public void FailsWhenEmpty()
        {
            Assert.Throws<Exception>(() =>
                new FailEmptyOpt(new IOpt<int>.Empty()).Go()
            );
        }

        [Fact]
        public void ThrowsSpecificException()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
                new FailEmptyOpt(
                    new IOpt<int>.Empty(),
                    new IndexOutOfRangeException()
                ).Go()
            );
        }
    }
}
