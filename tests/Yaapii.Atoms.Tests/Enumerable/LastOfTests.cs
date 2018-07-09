using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Enumerable.Tests
{
    public sealed class LastOfTests
    {
        [Fact]
        public void ReturnsLastValue()
        {
            var list = new EnumerableOf<string>("hallo", "ich", "heisse", "Max");

            Assert.Equal(
                "Max",
                new LastOf<string>(list).Value()
            );
        }
    }
}
