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
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Scalar.Tests
{
    public sealed class FirstOfTests
    {
        [Fact]
        public void ThrowsCustomException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new FirstOf<string>(
                    new ManyOf<string>(),
                    new InvalidOperationException()
                ).Value()
            );
        }

        [Fact]
        public void ReturnsFallBack()
        {
            Assert.Equal(
                "gotcha",
                new FirstOf<string>(
                    new ManyOf(), "gotcha").Value()
                );
        }

        [Fact]
        public void ReturnsFirstMatch()
        {
            var list = new ManyOf<string>("hallo", "ich", "heisse", "Max");

            Assert.Equal(
                "Max",
                new FirstOf<string>(item => item.StartsWith("M"), list).Value()
            );
        }

        [Fact]
        public void ReturnsFirstValue()
        {
            var list = new ManyOf<string>("hallo", "ich", "heisse", "Max");

            Assert.Equal(
                "hallo",
                new FirstOf<string>(list).Value()
            );
        }
    }
}
