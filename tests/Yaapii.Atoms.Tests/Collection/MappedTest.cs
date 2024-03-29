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
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Collection.Tests
{
    public sealed class MappedTest
    {

        [Fact]
        public void BehavesAsCollection()
        {
            Assert.Contains(
                0,
                new Mapped<int, int>(
                    i => i + 1,
                    new ManyOf<int>(-1, 1, 2)
                ));
        }

        [Fact]
        public void TransformsList()
        {
            Assert.Contains(
                "HELLO",
                new Mapped<string, string>(
                    input =>
                    input.ToUpper(),
                    new ManyOf<string>("hello", "world", "друг")
                )
            );
        }

        [Fact]
        public void TransformsEmptyList()
        {
            Assert.Empty(
                new Mapped<String, IText>(
                    input => new Upper(new LiveText(input)),
                    new List<string>()
                ));
        }

    }
}
