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
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Misc;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable.Tests
{
    public sealed class LimitedTest
    {
        [Fact]
        public void EnumeratesOverPrefixOfGivenLength()
        {
            Assert.True(
                new SumOfInts(
                new Limited<int>(
                    new EnumerableOf<int>(0, 1, 2, 3, 4),
                    3
                )).Value() == 3,
            "Can't limit an enumerable with more items");
        }

        [Fact]
        public void IteratesOverWholeEnumerableIfThereAreNotEnoughItems()
        {
            Assert.True(
                new SumOfInts(
                new Limited<int>(
                    new EnumerableOf<int>(0, 1, 2, 3, 4, 5),
                    10
                )).Value() == 15,
            "Can't limit an enumerable with less items");
        }

        [Fact]
        public void LimitOfZeroProducesEmptyEnumerable()
        {
            Assert.True(
                new LengthOf(
                    new Limited<int>(
                        new EnumerableOf<int>(0, 1, 2, 3, 4),
                    0
                )).Value() == 0,
            "Can't limit an iterable to zero items");
        }

        [Fact]
        public void NegativeLimitProducesEmptyEnumerable()
        {
            Assert.True(
                new LengthOf(
                    new Limited<int>(
                        new EnumerableOf<int>(0, 1, 2, 3, 4),
                        -1
                    )).Value() == 0,
                "Can't limit an iterable to negative number of items");
        }

        [Fact]
        public void EmptyEnumerableProducesEmptyEnumerable()
        {
            Assert.True(
                new LengthOf(
                    new Limited<Nothing>(
                        new EnumerableOf<Nothing>(),
                        10
                    )).Value() == 0,
            "Can't limit an empty enumerable");
        }
    }
}
