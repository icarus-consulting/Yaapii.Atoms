using System;
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

        [Theory]
        [InlineData(0, -5)]
        [InlineData(1, 6)]
        [InlineData(2, 1)]
        public void SortsByFunction(int index, int expectedKey)
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
            Assert.Equal(expectedKey, sortedArr[index].Key);
        }

        [Theory]
        [InlineData(0, -5)]
        [InlineData(1, 1)]
        [InlineData(2, 6)]
        public void DefaultComparerSeemsSane(int index, int expectedKey)
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
            Assert.Equal(expectedKey, keys[index]);
        }

        [Theory]
        [InlineData(0, -5)]
        [InlineData(1, 1)]
        [InlineData(2, 6)]
        public void EnumeratesKeysWhenLazy(int index, int expectedKey)
        {
            var unsorted = new LazyDict<int, int>(false,
                new Kvp.Of<int, int>(1, () => { throw new Exception("i shall not be called"); }),
                new Kvp.Of<int, int>(6, () => { throw new Exception("i shall not be called"); }),
                new Kvp.Of<int, int>(-5, () => { throw new Exception("i shall not be called"); })
            );
            var sorted = new Sorted<int, int>(unsorted);
            var keys = new int[3];
            sorted.Keys.CopyTo(keys, 0);
            Assert.Equal(expectedKey, keys[index]);
        }

        [Fact]
        public void DeliversSingleValueWhenLazy()
        {
            var unsorted = new LazyDict<int, int>(false,
                new Kvp.Of<int, int>(1, () => 4),
                new Kvp.Of<int, int>(6, () => { throw new Exception("i shall not be called"); }),
                new Kvp.Of<int, int>(-5, () => { throw new Exception("i shall not be called"); })
            );
            var sorted = new Sorted<int, int>(unsorted);
            Assert.Equal(4, sorted[1]);
        }

        [Fact]
        public void RejectsBuildingAllValuesByDefault()
        {
            var unsorted = new LazyDict<int, int>(false,
                new Kvp.Of<int, int>(1, () => 4),
                new Kvp.Of<int, int>(6, () => { throw new Exception("i shall not be called"); }),
                new Kvp.Of<int, int>(-5, () => { throw new Exception("i shall not be called"); })
            );
            var sorted = new Sorted<int, int>(unsorted);
            var ex = Assert.Throws<InvalidOperationException>(() => sorted.Values.GetEnumerator());
            Assert.Equal("Cannot get all values because this is a lazy dictionary. Getting the values would build all keys. If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.", ex.Message);
        }
    }
}
