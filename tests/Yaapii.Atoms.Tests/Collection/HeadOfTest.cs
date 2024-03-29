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
using Yaapii.Atoms.Fail;

namespace Yaapii.Atoms.Collection.Tests
{
    public sealed class HeadOfTest
    {

        [Fact]
        public void BehavesAsCollection()
        {
            Assert.Contains(
                new HeadOf<int>(
                    2,
                    new ManyOf<int>(1, -1, 2, 0)
                ),
                predicate => predicate.Equals(-1));
        }

        [Fact]
        public void Size()
        {
            Assert.Equal(
                2,
                new HeadOf<string>(
                    2,
                    new ManyOf<string>(
                        "hello", "world", "друг")
                ).Count);
        }

        [Fact]
        public void SizeEmptyReturnZero()
        {
            Assert.Empty(
                new HeadOf<int>(
                    2,
                    new List<int>()
                )
            );
        }

        [Fact]
        public void SizeLimitZeroReturnZero()
        {
            Assert.Empty(
                new HeadOf<string>(
                    0,
                    new ManyOf<string>("1", "2", "3")
                )
            );
        }

        [Fact]
        public void WithItemsNotEmpty()
        {
            Assert.NotEmpty(
                new HeadOf<String>(
                    2,
                    new ManyOf<string>("first", "second")
                ));
        }

        [Fact]
        public void WithoutItemsIsEmpty()
        {
            Assert.Empty(
                new HeadOf<String>(
                    0, new ManyOf<string>("third", "fourth")
                ));
        }

        [Fact]
        public void RejectsAdd()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
            new HeadOf<int>(
                2,
                new ManyOf<int>(1, 2, 3, 4)
            ).Add(6));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
               new HeadOf<int>(
                   2,
                   new ManyOf<int>(1, 2, 3, 4)
               ).Remove(1));
        }

        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new HeadOf<int>(
                    2, new ManyOf<int>(1, 2, 3, 4)
                ).Clear());
        }
    }
}
