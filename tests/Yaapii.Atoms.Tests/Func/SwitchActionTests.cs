// MIT License
//
// Copyright(c) 2025 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.Func.Tests
{
    public sealed class ActionSwitchTests
    {
        [Theory]
        [InlineData("Ulf", 10)]
        [InlineData("Rolf", 20)]
        public void SingleSwitchActionSwitches(string name, int expected)
        {
            var result = 0;
            new ActionSwitch<int>(
                new ActionIf<int>("Ulf", ipt => result += ipt),
                new ActionIf<int>("Rolf", ipt => result += ipt * 2)
            ).Invoke(name, 10);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SingleSwitchActionSwitchesToFallback()
        {
            var result = string.Empty;
            new ActionSwitch<int>(
                new ActionIf<int>("Ulf", ipt => result += ipt),
                new ActionIf<int>("Rolf", ipt => result += ipt * 2),
                (unknown, ipt) => result = $"{unknown} {ipt}"
            ).Invoke("Ignatius", 10);

            Assert.Equal("Ignatius 10", result);
        }

        [Fact]
        public void RejectsIfNoFallbackGiven()
        {
            Assert.Throws<ArgumentException>(() =>
                new ActionSwitch<int>(
                    new ActionIf<int>("Ulf", ipt => { }),
                    new ActionIf<int>("Rolf", ipt => { })
                ).Invoke("BOOM", int.MaxValue)
            );
        }

        [Theory]
        [InlineData("Add", 5)]
        [InlineData("Multiply", 6)]
        public void DualSwitchActionSwitches(string op, int expected)
        {
            var result = 0;
            new ActionSwitch<int, int>(
                new ActionIf<int, int>("Add", (i1, i2) => result = i1 + i2),
                new ActionIf<int, int>("Multiply", (i1, i2) => result = i1 * i2)
            ).Invoke(op, 2, 3);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void DualSwitchActionSwitchesToFallback()
        {
            var result = string.Empty;
            new ActionSwitch<int, int>(
                new ActionIf<int, int>("Ulf", (i1, i2) => result += i1 + i2),
                new ActionIf<int, int>("Rolf", (i1, i2) => result += i1 * i2),
                (unknown, i1, i2) => result = $"{unknown} {i1} {i2}"
            ).Invoke("Ignatius", 5, 7);

            Assert.Equal("Ignatius 5 7", result);
        }
    }
}
