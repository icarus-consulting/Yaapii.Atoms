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

using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Enumerator;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Enumerator.Tests
{
    public sealed class HeadOfTest
    {
        [Fact]
        public void LimitsContent()
        {
            Assert.True(
                new Joined(
                    ", ",
                    new ManyOf<IText>(
                        new Mapped<int, IText>(
                            new HeadOf<int>(
                                new ManyOf<int>(1, 2, 3, 4).GetEnumerator(),
                                2
                            ), 
                            str => new TextOf(str + "")
                        )
                    )
                ).AsString() == "1, 2",
                "cannot limit enumertor contents"
            );
                
        }
    }
}
