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
        public void NextNeighbour()
        {
            Assert.True(
            new ItemNeighbour<int>(
                2,
                new EnumerableOf<int>(1, 2, 3)
            ).Value() == 3,
            "Can't get the right neighbour from the enumerable"
        );
        }

        [Fact]
        public void NeighbourByPos()
        {
            Assert.True(
                new ItemNeighbour<int>(
                    2,
                    new EnumerableOf<int>(1, 2, 3),
                    -1
                ).Value() == 1,
            "Can't take the left neighbour from the enumerable");
        }

        [Fact]
        public void FailForInvalidPosition()
        {
            Assert.True(
                new ItemNeighbour<string>(
                    "1",
                    new EnumerableOf<string>("1", "2", "3"),
                    -1,
                    "15"
                ).Value() == "15");
        }

        [Fact]
        public void FailForEmptyCollection()
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

        [Fact]
        public void NeighbourWithCustomComparable()
        {
            var nb1 = new FakeNeighbour(DateTime.Parse("11.10.2017"));
            var nb2 = new FakeNeighbour(DateTime.Parse("10.10.2017"));
            var nb3 = new FakeNeighbour(DateTime.Parse("13.10.2017"));

            Assert.True(
                new ItemNeighbour<FakeNeighbour>(
                    nb1,
                    new EnumerableOf<FakeNeighbour>(nb1, nb2),
                    -1,
                    nb2
                ).Value().TimeStamp() == nb2.TimeStamp(),
            "Can't take the item by position from the enumerable");
        }

        internal class FakeNeighbour : IComparable<FakeNeighbour>
        {
            private readonly DateTime _stmp;

            public FakeNeighbour(DateTime stmp)
            {
                _stmp = stmp;
            }

            public DateTime TimeStamp() { return _stmp; }

            public int CompareTo(FakeNeighbour obj)
            {
                return _stmp.CompareTo(obj.TimeStamp());
            }
        }
    }
}
