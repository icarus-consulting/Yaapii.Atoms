using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Lookup.Tests
{
    public sealed class SortedTest
    {
        [Fact]
        public void SortsByFunction()
        {
            var unsorted = new Dictionary<int, int>()
            {
                {1, 4 },
                {6, 3 },
                {-5, 2}
            };

            var sorted = new Sorted<int, int>(unsorted, (a, b) => a.Value - b.Value);

            var sortedArr = new KeyValuePair<int, int>[3];
            sorted.CopyTo(sortedArr, 0);

            Assert.Equal(2, sortedArr[0].Value);
            Assert.Equal(3, sortedArr[1].Value);
            Assert.Equal(4, sortedArr[2].Value);

            Assert.Equal(-5, sortedArr[0].Key);
            Assert.Equal(6, sortedArr[1].Key);
            Assert.Equal(1, sortedArr[2].Key);
        }

        [Fact]
        public void DefaultComparerSeemsSane()
        {
            var unsorted = new Dictionary<int, int>()
            {
                {1, 4 },
                {6, 3 },
                {-5, 2}
            };

            var sorted = new Sorted<int, int>(unsorted);

            var sortedArr = new KeyValuePair<int, int>[3];
            sorted.CopyTo(sortedArr, 0);

            Assert.Equal(-5, sortedArr[0].Key);
            Assert.Equal(1, sortedArr[1].Key);
            Assert.Equal(6, sortedArr[2].Key);

            Assert.Equal(2, sortedArr[0].Value);
            Assert.Equal(4, sortedArr[1].Value);
            Assert.Equal(3, sortedArr[2].Value);
        }
    }
}
