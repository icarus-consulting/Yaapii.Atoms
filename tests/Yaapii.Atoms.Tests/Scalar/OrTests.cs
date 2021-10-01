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

using System.Collections.Generic;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Scalar.Tests
{
    public sealed class OrTests
    {
        [Fact]
        public void TrueOrGenEnumerable()
        {
            Assert.True(
                new Or(
                    new ManyOf<IScalar<bool>>(
                        new Live<bool>(true),
                        new Live<bool>(false)
                    )
                ).Value()
            );
        }

        [Fact]
        public void TrueOrGenFuncs()
        {
            Assert.True(
                new Or<bool>(
                    func => func,
                    new True().Value(),
                    new False().Value()
                    ).Value() == true
            );
        }

        [Fact]
        public void TrueOrGenScalar()
        {
            Assert.True(
                new Or(
                    new True(),
                    new False(),
                    new True()
                    ).Value() == true
            );
        }

        [Fact]
        public void WorksWithFuncAndParamItems()
        {
            Assert.True(
                    new Or<int>(
                        input => input > 0,
                        1, -1, 0
                    ).Value());
        }

        [Fact]
        public void WorksWithFuncAndListItems()
        {
            Assert.True(
                    new Or<int>(
                        input => input > 0,
                        new List<int>() { 1, -1, 0 }
                    ).Value());
        }

        [Fact]
        public void WorksWithIFunc()
        {
            Assert.False(
                    new Or<int>(
                        new FuncOf<int, bool>(input => input > 0),
                        -1, -2, -3
                    ).Value());
        }

        [Theory]
        [InlineData("DB", true)]
        [InlineData("ABC", true)]
        [InlineData("DEF", false)]
        public void WorksWithValueAndFunctions(string value, bool expected)
        {
            var or =
                new Or<string>(
                    value,
                    str => str.Contains("A"),
                    str => str.Contains("B"),
                    str => str.Contains("C"));

            Assert.Equal(expected, or.Value());
        }

        [Fact]
        public void InputBoolValuesToTrue()
        {
            Assert.True(new Or(false, true, false).Value());
        }

        [Fact]
        public void InputBoolValuesToFalse()
        {
            Assert.False(new Or(new List<bool>() { false, false, false }).Value());
        }

        [Fact]
        public void InputBoolFunctionsToTrue()
        {
            Assert.True(new Or(() => true, () => false).Value());
        }
    }
}