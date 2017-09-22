/// MIT License
///
/// Copyright(c) 2017 ICARUS Consulting GmbH
///
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
///
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
///
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.

using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class TernaryTest
    {

        [Fact]
        public void ConditionTrue()
        {
            Assert.True(
                new Ternary<bool, int>(
                    new True(),
                    6,
                    16
                ).Value() == 6);
        }

        [Fact]
        public void ConditionFalse()
        {
            Assert.True(
                new Ternary<bool, int>(
                    new False(),
                    6,
                    16
                ).Value() == 16);
        }

        [Fact]
        public void ConditionBoolean()
        {
            Assert.True(
            new Ternary<bool, int>(
                true,
                6,
                16
            ).Value() == 6);
        }

        [Fact]
        public void ConditionFunc()
        {
            Assert.True(
                new Ternary<int, int>(
                    5,
                    input => input > 3,
                    input => input = 8,
                    input => input = 2
                ).Value() == 8);
        }
    }
}