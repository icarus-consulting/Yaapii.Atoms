// MIT License
//
// Copyright(c) 2019 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Fail;

namespace Yaapii.Atoms.Collection.Tests
{
    public sealed class JoinedTest
    {
        [Fact]
        public void BehavesAsCollection()
        {
            new Joined<int>(
                new Many.Of<int>(1, -1, 2, 0),
                new Many.Of<int>(1, -1, 2, 0),
                new Many.Of<int>(1, -1, 2, 0)
            );
        }

        [Fact]
        public void Size()
        {
            Assert.Equal(
                8,
                new Joined<String>(
                    new Many.Of<string>("hello", "world", "друг"),
                    new Many.Of<string>("how", "are", "you"),
                    new Many.Of<string>("what's", "up")
                ).Count);
        }

        [Fact]
        public void SizeEmptyReturnZero()
        {
            Assert.Empty(
                new Joined<String>(
                    new List<string>()
                ));
        }

        [Fact]
        public void WithItemsNotEmpty()
        {
            Assert.NotEmpty(
                new Joined<String>(
                    new Many.Of<string>("1", "2"),
                    new Many.Of<string>("3", "4")
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
                 new Many.Of<int>(1, 2),
                 new Many.Of<int>(3, 4),
                 new Many.Of<int>(5, 6)
             ).Add(7));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new Joined<String>(
                    new Many.Of<string>("w", "a"),
                    new Many.Of<string>("b", "c")
                ).Remove("t"));
        }

        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new Joined<int>(
                    new Many.Of<int>(10),
                    new Many.Of<int>(20)
                ).Clear());
        }
    }
}