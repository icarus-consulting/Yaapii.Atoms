﻿// MIT License
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
    public sealed class ReversedTest
    {

        [Fact]
        public void BehavesAsCollection()
        {
            Assert.True(
                new ItemAt<int>(
                    new Reversed<int>(
                        new Enumerable.EnumerableOf<int>(0, -1, 2))
                ).Value() == 2,
            "cannot behave as a collection");
        }

        [Fact]
        public void ReversesList()
        {
            String last = "last";
            Assert.True(
                new ItemAt<string>(
                    new Reversed<string>(
                        new Enumerable.EnumerableOf<string>(
                            "item", last)
                    )).Value() == last);
        }

        [Fact]
        public void ReversesEmptyList()
        {
            Assert.Empty(
                new Reversed<string>(
                    new List<string>()));
        }

        [Fact]
        public void Size()
        {
            Assert.True(
                new Reversed<string>(
                    new Enumerable.EnumerableOf<string>(
                        "0", "1", "2")
                ).Count == 3);
        }

        [Fact]
        public void NotEmpty()
        {
            Assert.NotEmpty(
                new Reversed<int>(
                    new Enumerable.EnumerableOf<int>(
                        6, 16
                    )
                ));
        }

        [Fact]
        public void Contains()
        {
            String word = "objects";

            Assert.Contains(
                word,
                new Reversed<string>(
                    new Enumerable.EnumerableOf<string>(
                        "hello", "elegant", word)
                ));
        }

        [Fact]
        public void RejectsAdd()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
              new Reversed<int>(
                  new Enumerable.EnumerableOf<int>(
                      1, 2, 3, 4)
              ).Add(6));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new Reversed<int>(
                    new Enumerable.EnumerableOf<int>(
                        1, 2, 3, 4
                    )
                ).Remove(1));
        }



        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new Reversed<int>(
                    new Enumerable.EnumerableOf<int>(
                        1, 2, 3, 4)
                ).Clear());
        }
    }
}
