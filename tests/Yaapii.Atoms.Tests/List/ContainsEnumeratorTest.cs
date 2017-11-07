using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.List.Tests
{
    public class ContainsEnumeratorTest
    {
        [Fact]
        public void FindsItem()
        {
            Assert.True(
                new ContainsEnumerator<string>(
                    new EnumerableOf<string>("Hello", "my", "cat", "is", "missing").GetEnumerator(),
                    "cat"
                    ).Value());
        }

        [Fact]
        public void DoesntFindItem()
        {
            Assert.False(
                new ContainsEnumerator<string>(
                    new EnumerableOf<string>("Hello", "my", "cat", "is", "missing").GetEnumerator(),
                    "elephant"
                    ).Value());
        }
    }
}
