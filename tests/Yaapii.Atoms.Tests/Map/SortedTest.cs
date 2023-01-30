// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Map.Tests

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

        [Fact]
        public void ListsValuesFromGenericKeyAndValue()
        {
            Assert.Equal(
                ManyOf.New(2, 1),
                new Sorted<string, int>(
                    MapOf.New(
                        "b", 1,
                        "a", 2
                     ),
                    StringComparer.CurrentCultureIgnoreCase
                ).Values
            );

            Assert.Equal(
                ManyOf.New(2, 1),
                new Sorted<int>(
                    MapOf.New(
                        "b", 1,
                        "a", 2
                     ),
                    StringComparer.CurrentCultureIgnoreCase
                ).Values
            );

            Assert.Equal(
                ManyOf.New("2", "1"),
                new Sorted(
                    MapOf.New(
                        "b", "1",
                        "a", "2"
                     ),
                    StringComparer.CurrentCultureIgnoreCase
                ).Values
            );
        }

        [Fact]
        public void ListsValuesFromGenericValue()
        {
            Assert.Equal(
                ManyOf.New(2, 1),
                new Sorted<int>(
                    MapOf.New(
                        "b", 1,
                        "a", 2
                     ),
                    StringComparer.CurrentCultureIgnoreCase
                ).Values
            );
        }

        [Fact]
        public void ListsValuesFromStringPairs()
        {
            Assert.Equal(
                ManyOf.New("2", "1"),
                new Sorted(
                    MapOf.New(
                        "b", "1",
                        "a", "2"
                     ),
                    StringComparer.CurrentCultureIgnoreCase
                ).Values
            );
        }
    }
}
