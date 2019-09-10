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
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Scalar.Tests
{
    public sealed class MaxTest
    {
        [Fact]
        public void MaxAmongEmptyTest()
        {
            Assert.Throws<NoSuchElementException>(
                () => new Max<int>(new Many.Of<int>()).Value());
        }

        [Fact]
        public void MaxAmongOneTest()
        {
            int num = 10;
            Assert.True(
                new Max<int>(() => num).Value() == num,
                "Can't find the greater among one"
            );
        }

        [Fact]
        public void MaxAmongManyTest()
        {
            int num = 10;
            Assert.True(
                new Max<int>(
                    () => num,
                    () => 0,
                    () => -1,
                    () => 2
                 ).Value() == num,
                "Can't find the greater among many");
        }
    }
}
