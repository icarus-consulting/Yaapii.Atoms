using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Misc;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class SortedTest
    {
        [Fact]
        public void SortsAnArray()
        {
            Assert.True(
                new JoinedText(", ",
                new Mapped<int, string>(
                    new Sorted<int>(
                        new EnumerableOf<int>(3, 2, 10, 44, -6, 0)
                    ),
                    i => i.ToString())).AsString() == "-6, 0, 2, 3, 10, 44",
                "Can't sort an enumerable");
        }

        [Fact]
        public void SortsAnArrayWithComparator()
        {
            Assert.True(
                new JoinedText(", ",
                    new Sorted<string>(
                        IReverseComparer<string>.Default,
                        new EnumerableOf<string>(
                            "a", "c", "hello", "dude", "Friend"
                        )
                    )).AsString() == "hello, Friend, dude, c, a",
                "Can't sort an enumerable with a custom comparator");
        }

        [Fact]
        public void SortsAnEmptyArray()
        {
            Assert.Empty(
                new Sorted<int>(
                    new EnumerableOf<int>()
                ));
        }
    }
}
