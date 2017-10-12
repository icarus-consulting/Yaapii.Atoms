using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Tests.List
{
    public class SiblingTest
    {
        [Fact]
        public void Next()
        {
            Assert.True(
            new Sibling<int>(
                2,
                new EnumerableOf<int>(1, 2, 3)
            ).Value() == 3,
            "Can't get the right neighbour from the enumerable"
        );
        }

        [Fact]
        public void ByPos()
        {
            Assert.True(
                new Sibling<int>(
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
                new Sibling<string>(
                    "1",
                    new EnumerableOf<string>("1", "2", "3"),
                    -1,
                    "15"
                ).Value() == "15");
        }

        [Fact]
        public void FailForEmptyCollection()
        {
            Assert.Throws<IOException>(
                () =>
                    new Sibling<int>(
                        1337,
                        new EnumerableOf<int>()
                ).Value());
        }

        [Fact]
        public void FallbackTest()
        {
            String fallback = "fallback";
            Assert.True(
                new Sibling<string>(
                    "Not-there",
                    new EnumerableOf<string>(),
                    12,
                    fallback
                ).Value() == fallback,
            "Can't fallback to default value");
        }

        [Fact]
        public void WithCustomComparable()
        {
            var format = "dd.MM.yyyy";
            var provider = CultureInfo.InvariantCulture;
            var nb1 = new FakeSibling(DateTime.ParseExact("11.10.2017", format, provider));
            var nb2 = new FakeSibling(DateTime.ParseExact("10.10.2017", format, provider));
            var nb3 = new FakeSibling(DateTime.ParseExact("13.10.2017", format, provider));

            Assert.True(
                new Sibling<FakeSibling>(
                    nb1,
                    new EnumerableOf<FakeSibling>(nb1, nb2),
                    -1,
                    nb2
                ).Value().TimeStamp() == nb2.TimeStamp(),
            "Can't take the item by position from the enumerable");
        }

        internal class FakeSibling : IComparable<FakeSibling>
        {
            private readonly DateTime _stmp;

            public FakeSibling(DateTime stmp)
            {
                _stmp = stmp;
            }

            public DateTime TimeStamp() { return _stmp; }

            public int CompareTo(FakeSibling obj)
            {
                return _stmp.CompareTo(obj.TimeStamp());
            }
        }
    }
}
