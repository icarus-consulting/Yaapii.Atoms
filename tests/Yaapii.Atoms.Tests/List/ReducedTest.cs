using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class ReducedTest
    {
        [Fact]
        public void SkipIterable()
        {
            Assert.True(
                new Reduced<int, int>(
                    new EnumerableOf<int>(1, 1, 2, 2, 3, 4, 5, 6),
                    0,
                    (first, second) => first + second
                ).Value() == 24,
            "cannot reduce enumerable");
        }
    }
}
