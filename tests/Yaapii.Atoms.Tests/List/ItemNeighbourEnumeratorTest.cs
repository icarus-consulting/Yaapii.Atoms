using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Tests.List
{
    public class ItemNeighbourEnumeratorTest
    {
        [Fact]
        public void RightNeighbourTest()
        {
            Assert.True(
            new ItemNeighbourEnumerator<int>(
                new EnumerableOf<int>(1, 2, 3).GetEnumerator(),
                2,
                1
            ).Value() == 3,
            "Can't take the right neighbour from the enumerator");
        }

        [Fact]
        public void LeftNeighbourTest()
        {
            Assert.True(
            new ItemNeighbourEnumerator<int>(
                new EnumerableOf<int>(1, 2, 3).GetEnumerator(),
                2,
                -1
            ).Value() == 1,
            "Can't take the left neighbour from the enumerator");
        }

        [Fact]
        public void NeighbourByPosTest()
        {
            Assert.True(
            new ItemNeighbourEnumerator<int>(
                new EnumerableOf<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10).GetEnumerator(),
                5,
                4
            ).Value() == 9,
            "Can't take the neighbour by position from the enumerator");
        }

        //[Fact]
        //public void ElementByPosTest()
        //{
        //    Assert.True(
        //        new ItemAtEnumerator<int>(
        //            new EnumerableOf<int>(1, 2, 3).GetEnumerator(),
        //            1
        //        ).Value() == 2,
        //        "Can't take the item by position from the enumerator");
        //}

        [Fact]
        public void FailForEmptyCollectionTest()
        {
            Assert.Throws(
                typeof(IOException),
                () => new ItemNeighbourEnumerator<int>(
                        new EnumerableOf<int>(new int[0]).GetEnumerator(),
                        1,
                        1
                        ).Value());
        }

        [Fact]
        public void FailForOutOfRangePositionNegTest()
        {
            Assert.Throws(
                typeof(IOException),
                    () => new ItemNeighbourEnumerator<int>(
                        new EnumerableOf<int>(1, 2, 3).GetEnumerator(),
                        2,
                        -100
            ).Value());
        }

        [Fact]
        public void FailForOutOfRangePositionPosTest()
        {
            Assert.Throws(
                typeof(IOException),
                    () => new ItemNeighbourEnumerator<int>(
                        new EnumerableOf<int>(1, 2, 3).GetEnumerator(),
                        2,
                        100
            ).Value());
        }

        [Fact]
        public void FailForItemNotInEnumeratorTest()
        {
            Assert.Throws(
                typeof(IOException),
                    () => new ItemNeighbourEnumerator<int>(
                        new EnumerableOf<int>(1, 2, 3).GetEnumerator(),
                        4,
                        1
            ).Value());
        }

        [Fact]
        public void FallbackTest()
        {
            string fallback = "fallback";

            Assert.True(
            new ItemNeighbourEnumerator<string>(
                new EnumerableOf<string>().GetEnumerator(),
                "searchthis",
                fallback
            ).Value() == fallback,
            "Can't fallback to default value");
        }
    }
}
