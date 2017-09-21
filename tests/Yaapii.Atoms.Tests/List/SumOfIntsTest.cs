using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class SumOfIntsTest
    {
        [Fact]
        public void WithVarargsCtor()
        {
            Assert.True(
                new SumOfInts(
                1, 2, 3
            ).Value() == 6);
        }

        [Fact]
        public void WithIterCtor()
        {
            Assert.True(
            new SumOfInts(
                new EnumerableOf<int>(7, 8, 10)
            ).Value() == 25);
        }
    }
}