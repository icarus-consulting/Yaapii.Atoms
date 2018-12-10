﻿// MIT License
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
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Enumerable.Tests
{
    public sealed class DistinctTest
    {
        [Fact]
        public void MergesEntries()
        {
            Assert.True(
                new LengthOf(
                    new Distinct<int>(
                        new EnumerableOf<int>(1, 2, 3),
                        new EnumerableOf<int>(10, 2, 30)
                    )
                ).Value() == 5);
        }

        [Fact]
        public void MergesEntriesWithEnumCtor()
        {
            Assert.True(
                new LengthOf(
                    new Distinct<int>(
                        new EnumerableOf<IEnumerable<int>>(
                            new EnumerableOf<int>(1, 2, 3),
                            new EnumerableOf<int>(10, 2, 30)
                        )
                    )
                ).Value() == 5);
        }

        [Fact]
        public void WorksWithEmpties()
        {
            Assert.True(
                new LengthOf(
                    new Distinct<string>(
                        new EnumerableOf<string>(),
                        new EnumerableOf<string>()
                    )
                ).Value() == 0);
        }

        [Fact]
        public void DoubleRunDistinct()
        {
            var dst =
                new Distinct<string>(
                    new EnumerableOf<string>("test", "test")
                );
            Assert.True(
                new LengthOf(dst).Value() == new LengthOf(dst).Value());
        }
    }
}