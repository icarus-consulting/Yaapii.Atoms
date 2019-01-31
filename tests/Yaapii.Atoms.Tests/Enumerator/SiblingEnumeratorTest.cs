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
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Enumerator;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Enumerator.Tests
{
    public class SiblingEnumeratorTest
    {
        [Fact]
        public void RightNeighbourTest()
        {
            Assert.True(
            new SiblingEnumerator<int>(
                new EnumerableOf<int>(1, 2, 3).GetEnumerator(),
                2,
                1
            ).Value() == 3,
            "Can't take the right neighbour from the enumerator");
        }

        [Fact]
        public void LeftNeighbourTest()
        {
            Assert.True(
            new SiblingEnumerator<int>(
                new EnumerableOf<int>(1, 2, 3).GetEnumerator(),
                2,
                -1
            ).Value() == 1,
            "Can't take the left neighbour from the enumerator");
        }

        [Fact]
        public void NeighbourByPosTest()
        {
            Assert.True(
            new SiblingEnumerator<int>(
                new EnumerableOf<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10).GetEnumerator(),
                5,
                4
            ).Value() == 9,
            "Can't take the neighbour by position from the enumerator");
        }

        //[Fact]
        //public void ElementByPosTest()
        //{
        //    Assert.True(
        //        new ItemAtEnumerator<int>(
        //            new EnumerableOf<int>(1, 2, 3).GetEnumerator(),
        //            1
        //        ).Value() == 2,
        //        "Can't take the item by position from the enumerator");
        //}

        [Fact]
        public void FailForEmptyCollectionTest()
        {
            Assert.Throws<IOException>(
                () => new SiblingEnumerator<int>(
                        new EnumerableOf<int>(new int[0]).GetEnumerator(),
                        1,
                        1
                        ).Value());
        }

        [Fact]
        public void FailForOutOfRangePositionNegTest()
        {
            Assert.Throws<IOException>(
                    () => new SiblingEnumerator<int>(
                        new EnumerableOf<int>(1, 2, 3).GetEnumerator(),
                        2,
                        -100
            ).Value());
        }

        [Fact]
        public void FailForOutOfRangePositionPosTest()
        {
            Assert.Throws<IOException>(
                    () => new SiblingEnumerator<int>(
                        new EnumerableOf<int>(1, 2, 3).GetEnumerator(),
                        2,
                        100
            ).Value());
        }

        [Fact]
        public void FailForItemNotInEnumeratorTest()
        {
            Assert.Throws<IOException>(
                    () => new SiblingEnumerator<int>(
                        new EnumerableOf<int>(1, 2, 3).GetEnumerator(),
                        4,
                        1
            ).Value());
        }

        [Fact]
        public void FallbackTest()
        {
            string fallback = "fallback";

            Assert.True(
            new SiblingEnumerator<string>(
                new EnumerableOf<string>().GetEnumerator(),
                "searchthis",
                fallback
            ).Value() == fallback,
            "Can't fallback to default value");
        }
    }
}
