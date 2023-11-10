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
using System.Threading;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.List.Tests
{
    public sealed class LiveListTest
    {
        [Fact]
        public void WorksWithMultipleValues()
        {
            var list = new LiveList<string>("one", "two");
            Assert.NotNull(
                list[1]
            );
        }

        [Fact]
        public void KnowsIfEmpty()
        {
            Assert.Empty(
                new LiveList<int>(() =>
                    new List<int>()
                )
            );
        }

        [Fact]
        public void LowBoundTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new LiveList<int>(() =>
                    new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
                )
                [-1]
            );
        }

        [Fact]
        public void HighBoundTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new LiveList<int>(() =>
                    new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
                )
                [11]
            );
        }

        [Fact]
        public void SensesChangesFromArray()
        {
            var volatileArray = new int[] { 1, 2 };
            var live =
                new LiveList<int>(
                    volatileArray
                );

            var a = new List<int>(live);
            volatileArray[0] = 3;
            var b = new List<int>(live);
            Assert.NotEqual(a, b);
        }

        [Fact]
        public void SensesChangesFromFunc()
        {
            int size = 2;
            var list =
                new LiveList<int>(() =>
                    new ListOf<int>(
                        new HeadOf<int>(
                            new Endless<int>(size),
                            new Live<int>(() =>
                                {
                                    return Interlocked.Increment(ref size);
                                }
                            )
                        )
                    )
                );

            Assert.NotEqual(list.Count, list.Count);
        }

        [Fact]
        public void SensesChangesFromEnumerator()
        {
            int size = 2;
            var list =
                new LiveList<int>(
                    new ListOf<int>(
                        new HeadOf<int>(
                            new Endless<int>(1),
                            new Live<int>(() =>
                                Interlocked.Increment(ref size)
                            )
                        )
                    ).GetEnumerator()
                );

            Assert.NotEqual(list.Count, list.Count);
        }
    }
}
