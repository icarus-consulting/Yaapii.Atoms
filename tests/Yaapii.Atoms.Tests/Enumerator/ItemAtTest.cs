﻿// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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

using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Fail;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum

namespace Yaapii.Atoms.Enumerator.Tests
{
    public sealed class ItemAtTest
    {
        [Fact]
        public void FirstElementTest()
        {
            Assert.True(
            new ItemAt<int>(
                new ManyOf<int>(1, 2, 3).GetEnumerator()
            ).Value() == 1,
            "Can't take the first item from the enumerator");
        }

        [Fact]
        public void ElementByPosTest()
        {
            Assert.True(
                new ItemAt<int>(
                    new ManyOf<int>(1, 2, 3).GetEnumerator(),
                    1
                ).Value() == 2,
                "Can't take the item by position from the enumerator");
        }

        [Fact]
        public void FailForEmptyCollectionTest()
        {
            Assert.Throws<NoSuchElementException>(
                () => new ItemAt<int>(
                        new ManyOf<int>(new int[0]).GetEnumerator(),
                        0
                        ).Value());
        }

        [Fact]
        public void FailForNegativePositionTest()
        {
            Assert.Throws<NoSuchElementException>(
                    () => new ItemAt<int>(
                        new ManyOf<int>(1, 2, 3).GetEnumerator(),
                        -1
            ).Value());
        }

        [Fact]
        public void FallbackTest()
        {
            string fallback = "fallback";

            Assert.True(
            new ItemAt<string>(
                new ManyOf<string>().GetEnumerator(),
                fallback
            ).Value() == fallback,
            "Can't fallback to default value");
        }

        [Fact]
        public void FailForPosMoreLengthTest()
        {
            Assert.Throws<NoSuchElementException>(
                () =>
                new ItemAt<int>(
                    new LiveMany<int>(1, 2, 3).GetEnumerator(),
                    3
                ).Value()
            );
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
