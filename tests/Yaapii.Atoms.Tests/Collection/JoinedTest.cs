// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Fail;

namespace Yaapii.Atoms.Collection.Tests
{
    public sealed class JoinedTest
    {
        [Fact]
        public void BehavesAsCollection()
        {
            new Joined<int>(
                new Enumerable.EnumerableOf<int>(1, -1, 2, 0),
                new Enumerable.EnumerableOf<int>(1, -1, 2, 0),
                new Enumerable.EnumerableOf<int>(1, -1, 2, 0)
            );
        }

        [Fact]
        public void Size()
        {
            Assert.True(
                new Joined<String>(
                    new Enumerable.EnumerableOf<string>("hello", "world", "друг"),
                    new Enumerable.EnumerableOf<string>("how", "are", "you"),
                    new Enumerable.EnumerableOf<string>("what's", "up")
                ).Count == 8);
        }

        [Fact]
        public void SizeEmptyReturnZero()
        {
            Assert.True(
                new Joined<String>(
                    new List<string>()
                ).Count == 0);
        }

        [Fact]
        public void WithItemsNotEmpty()
        {
            Assert.NotEmpty(
                new Joined<String>(
                    new Enumerable.EnumerableOf<string>("1", "2"),
                    new Enumerable.EnumerableOf<string>("3", "4")
                ));
        }

        [Fact]
        public void WithoutItemsIsEmpty()
        {
            Assert.Empty(
                new Joined<String>(
                    new List<string>()));
        }

        [Fact]
        public void RejectsAdd()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
             new Joined<int>(
                 new Enumerable.EnumerableOf<int>(1, 2),
                 new Enumerable.EnumerableOf<int>(3, 4),
                 new Enumerable.EnumerableOf<int>(5, 6)
             ).Add(7));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new Joined<String>(
                    new Enumerable.EnumerableOf<string>("w", "a"),
                    new Enumerable.EnumerableOf<string>("b", "c")
                ).Remove("t"));
        }

        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new Joined<int>(
                    new Enumerable.EnumerableOf<int>(10),
                    new Enumerable.EnumerableOf<int>(20)
                ).Clear());
        }
    }
}