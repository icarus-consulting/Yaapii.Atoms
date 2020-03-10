﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Lookup.Tests
{
    public sealed class SortedTest
    {
        [Theory]
        [InlineData(1, 4)]
        [InlineData(6, 3)]
        [InlineData(-5, 2)]
        public void ValueStillBehindCorrectKeyDict(int key, int expectedValue)
        {
            var unsorted = new Dictionary<int, int>()
            {
                {1, 4 },
                {6, 3 },
                {-5, 2}
            };

            Assert.Equal(expectedValue, unsorted[key]);
            var sorted = new Sorted<int, int>(unsorted);
            Assert.Equal(expectedValue, sorted[key]);
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(6, 3)]
        [InlineData(-5, 2)]
        public void ValueStillBehindCorrectKeyIEnumerableKeyValuePairs(int key, int expectedValue)
        {
            var unsorted = new List<KeyValuePair<int, int>>()
            {
                new KeyValuePair<int, int>(1, 4),
                new KeyValuePair<int, int>(6, 3),
                new KeyValuePair<int, int>(-5, 2)
            };

            var sorted = new Sorted<int, int>(unsorted);
            Assert.Equal(expectedValue, sorted[key]);
        }

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

            var keys = new int[3];
            sorted.Keys.CopyTo(keys, 0);

            Assert.Equal(-5, keys[0]);
            Assert.Equal(1, keys[1]);
            Assert.Equal(6, keys[2]);
        }

        [Fact]
        public void DoesNotBuildValueWhenNotNeeded()
        {
            var unsorted = new LazyDict<int, int>(false,
                new Kvp.Of<int, int>(1, () => 4),
                new Kvp.Of<int, int>(6, () => { throw new Exception("i shall not be called"); }),
                new Kvp.Of<int, int>(-5, () => { throw new Exception("i shall not be called"); })
            );

            var sorted = new Sorted<int, int>(unsorted);

            var keys = new int[3];
            sorted.Keys.CopyTo(keys, 0);

            Assert.Equal(-5, keys[0]);
            Assert.Equal(1, keys[1]);
            Assert.Equal(6, keys[2]);

            // Sanety check: building one value isn't a problem
            Assert.Equal(4, sorted[1]);

            // Sanety check: but all of them is
            var ex = Assert.Throws<InvalidOperationException>(() => sorted.Values.GetEnumerator());
            Assert.Equal("Cannot get all values because this is a lazy dictionary. Getting the values would build all keys. If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.", ex.Message);
        }
    }
}
