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
using Yaapii.Atoms.List;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Func.Tests
{
    public class ChainedFuncTest
    {
        [Fact]
        public void WithoutIterable()
        {
            Assert.True(
            new LengthOf(
                new Filtered<string>(
                    input => input.EndsWith("XY"),
                    new Enumerable.Mapped<string, string>(
                        new ChainedFunc<String, String, String>(
                            input => input += "X",
                            input => input += "Y"
                        ),
                        new EnumerableOf<string>("public", "final", "class")
                    ))
            ).Value() == 3,
            "cannot chain functions");
        }

        [Fact]
        public void WithIterable()
        {
            Assert.True(
            new LengthOf(
                new Filtered<string>(
                    input => !input.StartsWith("st") && input.EndsWith("12"),
                     new Enumerable.Mapped<string, string>(
                        new ChainedFunc<string, string, string>(
                            input => input += "1",
                            new EnumerableOf<IFunc<string, string>>(
                                new FuncOf<string, string>(input => input += ("2")),
                                new FuncOf<string, string>(input => input.Replace("a", "b"))
                            ),
                            input => input.Trim()
                        ),
                        new EnumerableOf<string>("private", "static", "String")))
                 ).Value() == 2);
        }
    }
}
