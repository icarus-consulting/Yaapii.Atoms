// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
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

using System.Collections.Generic;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Enumerator
{
    public sealed class CachedTests
    {
        [Fact]
        public void DeliversMovementAbilityFromCache()
        {
            var advances = 0;
            var contents = new List<int>() { 1 };
            var enumerator =
                new Sticky<int>(
                    new Sticky<int>.Cache<int>(() => contents.GetEnumerator())
                );

            while (enumerator.MoveNext())
            {
                advances++;
            }

            contents.Clear();
            enumerator.Reset();
            Assert.True(enumerator.MoveNext());
        }

        [Fact]
        public void DeliversFromCache()
        {
            var advances = 0;
            var contents = new List<int>() { 1, 2, 3 };
            var enumerator =
                new Sticky<int>(
                    new Sticky<int>.Cache<int>(() => contents.GetEnumerator())
                );

            while (enumerator.MoveNext())
            {
                advances++;
            }

            contents.Clear();
            enumerator.Reset();

            var result = new List<int>();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);
            }
            Assert.Equal(new List<int>() { 1, 2, 3 }, result);
        }

        [Fact]
        public void DoesNotMoveWhenEmpty()
        {
            bool moved = false;
            var contents = new List<int>();
            var cache =
                new Sticky<int>.Cache<int>(() =>
                    new LoggingEnumerator<int>(
                        contents.GetEnumerator(),
                        idx => moved = true
                    )
                );

            var count = cache.Count;

            Assert.False(moved);
        }

        [Fact]
        public void CacheCachesItemCount()
        {
            var contents = new List<int>() { 1 };
            var cache = new Sticky<int>.Cache<int>(() => contents.GetEnumerator());

            var count = cache.Count;

            contents.Clear();

            Assert.True(cache.Count == 1);
        }
    }
}
