using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Tests.List
{
    public class ItemNeighbourTest
    {
        [Fact]
        public void NextNeighbourTest()
        {
            Assert.True(
            new ItemAt<int>(
                new EnumerableOf<int>(1, 2, 3)
            ).Value() == 1,
            "Can't take the first item from the enumerable"
        );
        }

        [Fact]
        public void NeighbourByPosTest()
        {
            Assert.True(
                new ItemAt<int>(
                    new EnumerableOf<int>(1, 2, 3),
                    1
                ).Value() == 2,
            "Can't take the item by position from the enumerable");
        }

        [Fact]
        public void FailForEmptyCollectionTest()
        {
            Assert.Throws(
                typeof(IOException),
                () => 
                    new ItemNeighbour<int>(
                        1337,
                        new EnumerableOf<int>()
                ).Value());
        }

        [Fact]
        public void FallbackTest()
        {
            String fallback = "fallback";
            Assert.True(
                new ItemNeighbour<string>(
                    "Not-there",
                    new EnumerableOf<string>(),
                    12,
                    fallback
                ).Value() == fallback,
            "Can't fallback to default value");
        }
    }
}
