using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.List.Tests
{
    public class ContainsTest
    {
        [Fact]
        public void FindsItem()
        {
            Assert.True(
                new Contains<string>(
                    new EnumerableOf<string>("Hello", "my", "cat", "is", "missing"),
                    (str) => str == "cat"
                    ).Value());
        }

        [Fact]
        public void DoesntFindItem()
        {
            Assert.False(
                new Contains<string>(
                    new EnumerableOf<string>("Hello", "my", "cat", "is", "missing"),
                    (str) => str == "elephant"
                    ).Value());
        }
    }
}
