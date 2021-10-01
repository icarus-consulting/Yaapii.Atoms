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

using System;
using System.Collections.Generic;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Fail;

namespace Yaapii.Atoms.Scalar.Tests
{
    public sealed class ItemAtTests
    {
        [Fact]
        public void DeliversFirstElement()
        {

            Assert.True(
                new ItemAt<int>(
                    new ManyOf<int>(1, 2, 3)
                ).Value() == 1,
                "Can't take the first item from the enumerable"
            );
        }

        [Fact]
        public void DeliversFirstElementWithException()
        {

            Assert.True(
                new ItemAt<int>(
                    new ManyOf<int>(1, 2, 3),
                    new NotFiniteNumberException("Cannot do this!")
                ).Value() == 1,
                "Can't take the first item from the enumerable"
            );
        }

        [Fact]
        public void DeliversElementByPos()
        {
            Assert.True(
                new ItemAt<int>(
                    new ManyOf<int>(1, 2, 3),
                    1
                ).Value() == 2,
                "Can't take the item by position from the enumerable"
            );
        }

        [Fact]
        public void DeliversElementByPosWithFallback()
        {
            Assert.True(
                new ItemAt<int>(
                    new ManyOf<int>(1, 2, 3),
                    1,
                    4
                ).Value() == 2,
                "Can't take the item by position from the enumerable"
            );
        }

        [Fact]
        public void FailsForEmptyCollection()
        {
            Assert.Throws<NoSuchElementException>(() =>
                new ItemAt<int>(
                    new List<int>()
                ).Value()
            );
        }

        [Fact]
        public void DeliversFallback()
        {
            String fallback = "fallback";
            Assert.True(
                new ItemAt<string>(
                    new ManyOf<string>(),
                    12,
                    fallback
                ).Value() == fallback,
            "Can't fallback to default value");
        }

        [Fact]
        public void FallbackShowsError()
        {
            Assert.Throws<NoSuchElementException>(() =>
                new ItemAt<string>(
                    new ManyOf<string>(),
                    12,
                    (ex, enumerable) => throw ex
                ).Value()
            );
        }

        [Fact]
        public void FallbackShowsGivenErrorWithPosition()
        {
            Assert.Throws<NotFiniteNumberException>(() =>
                new ItemAt<string>(
                    new ManyOf<string>(),
                    12,
                    new NotFiniteNumberException("Cannot do this!")
                ).Value()
            );
        }

        [Fact]
        public void FallbackShowsGivenErrorWithoutPosition()
        {
            Assert.Throws<NotFiniteNumberException>(() =>
                new ItemAt<string>(
                    new ManyOf<string>(),
                    new NotFiniteNumberException("Cannot do this!")
                ).Value()
            );
        }

        [Fact]
        public void FallbackShowsGivenErrorForNegativePosition()
        {
            Assert.Throws<NotFiniteNumberException>(() =>
                new ItemAt<string>(
                    new ManyOf<string>(),
                    -12,
                    new NotFiniteNumberException("Cannot do this!")
                ).Value()
            );
        }

        [Fact]
        public void IsSticky()
        {
            var list = new List<string>();
            list.Add("pre");
            var sticky = new ItemAt<string>(list);
            sticky.Value();
            list.Clear();
            list.Add("post");

            Assert.Equal("pre", sticky.Value());
        }
    }
}
