// MIT License
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

using System;
using Xunit;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.Func;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Yaapii.Atoms.Scalar.Tests
{
    public sealed class BitAtTests
    {
        [Fact]
        public void DeliversFirstElement()
        {
            Assert.True(
                new BitAt(new BytesOf(1)).Value()
            );
        }

        [Fact]
        public void DeliversElementAtPosition()
        {
            Assert.True(
                new BitAt(
                    new BytesOf(2),
                    1
                ).Value()
            );
        }

        [Fact]
        public void FailsWhenBytesEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                new BitAt(new BytesOf("")).Value()
            );
        }

        [Fact]
        public void FailsWithCustomExceptionOfFirstPosition()
        {
            Assert.Throws<ApplicationException>(() =>
                new BitAt(new BytesOf(""), new ApplicationException("Custom exception message")).Value()
            );
        }

        [Fact]
        public void DeliversFallback()
        {
            Assert.True(
                new BitAt(
                    new BytesOf(""),
                    2,
                    true
                ).Value()
            );
        }

        [Fact]
        public void DeliversFallbackOnFirstPosition()
        {
            Assert.True(
                new BitAt(
                    new BytesOf(""),
                    true
                ).Value()
            );
        }

        [Fact]
        public void ExecutesFallbackFunc()
        {
            Assert.True(
                new BitAt(
                    new BytesOf(""),
                    2,
                    bytes => true
                ).Value()
            );
        }

        [Fact]
        public void ExecutesFallbackIFunc()
        {
            Assert.True(
                new BitAt(
                    new BytesOf(""),
                    2,
                    new FuncOf<IBytes, bool>(bytes => true)
                ).Value()
            );
        }

        [Fact]
        public void ExecutesFallbackFuncOnFirstPosition()
        {
            Assert.True(
                new BitAt(
                    new BytesOf(""),
                    bytes => true
                ).Value()
            );
        }

        [Fact]
        public void UsesInjectedExcption()
        {
            Assert.Throws<ApplicationException>(() =>
                new BitAt(
                    new BytesOf(""),
                    1,
                    new ApplicationException()
                ).Value()
            );
        }
    }
}
