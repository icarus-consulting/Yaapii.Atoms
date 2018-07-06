using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Enumerable.Tests
{
    public sealed class FirstOfTests
    {
        [Fact]
        public void ReturnsFirstValue()
        {
            var list = new EnumerableOf<string>("hallo", "ich", "heisse", "Max");

            Assert.Equal(
                "hallo",
                new FirstOf<string>(list).Value()
            );
        }
    }
}
