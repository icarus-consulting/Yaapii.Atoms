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
                    "cat",
                    new EnumerableOf<string>("Hello", "my", "cat", "is", "missing")
                    ).Value());
        }

        [Fact]
        public void DoesntFindItem()
        {
            Assert.False(
                new Contains<string>(
                    "elephant",
                    new EnumerableOf<string>("Hello", "my", "cat", "is", "missing")
                    ).Value());
        }
    }
}
