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
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Texts;

namespace Yaapii.Atoms.Lists.Tests
{
    public sealed class MappedTest
    {
        [Fact]
        public void TransformsList()
        {
            Assert.True(
                new ItemAt<IText>(
                    new Mapped<String, IText>(
                        input => new Upper(new Text.Live(input)),
                        new List.Of<string>("hello", "world", "damn")
                        ),
                    0
                ).Value().AsString() == "HELLO",
            "Can't transform an enumerable");
        }

        [Fact]
        public void TransformsEmptyList()
        {
            Assert.True(
                new Enumerable.LengthOf(
                    new Mapped<String, IText>(
                        input => new Upper(new Text.Live(input)),
                        new List.Of<string>()
                    )).Value() == 0,
                "Can't transform an empty iterable");
        }
    }
}