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

namespace Yaapii.Atoms.Number.Tests
{
    public sealed class LiveNumberTests
    {
        [Fact]
        public void ConversionIsLive()
        {
            var text = "gkb";
            var number = new LiveNumber(() => text);
            text = "42";
            Assert.Equal(42, number.AsInt());
        }

        [Fact]
        public void ParsesFloat()
        {
            Assert.True(
                new LiveNumber(() => 4673.453F).AsFloat() == 4673.453F
            );
        }

        [Fact]
        public void RejectsNoFloatText()
        {
            Assert.Throws<ArgumentException>(() =>
                new LiveNumber(() => "ghki").AsFloat()
            );
        }

        [Fact]
        public void ParsesInt()
        {
            Assert.True(
                new LiveNumber(() => 1337).AsInt() == 1337
            );
        }

        [Fact]
        public void RejectsNoIntText()
        {
            Assert.Throws<ArgumentException>(() =>
                new LiveNumber(() => "ghki").AsInt()
            );
        }

        [Fact]
        public void ParsesDouble()
        {
            Assert.True(
                new LiveNumber(() => 843.23969274001D).AsDouble() == 843.23969274001D
            );
        }

        [Fact]
        public void RejectsNoDoubleText()
        {
            Assert.Throws<ArgumentException>(() =>
                new LiveNumber(() => "ghki").AsDouble()
            );
        }

        [Fact]
        public void ParsesLong()
        {
            Assert.True(
                new LiveNumber(() => 139807814253711).AsLong() == 139807814253711L
            );
        }

        [Fact]
        public void RejectsNoLongText()
        {
            Assert.Throws<ArgumentException>(() =>
                new LiveNumber(() => "ghki").AsLong()
            );
        }

        [Fact]
        public void IntToDouble()
        {
            Assert.True(
                new LiveNumber(
                    () => 5
                ).AsDouble() == 5d
            );
        }

        [Fact]
        public void DoubleToFloat()
        {
            Assert.True(
                new LiveNumber(
                    () => (551515155.451d)
                ).AsFloat() == 551515155.451f
            );
        }

        [Fact]
        public void FloatAsDouble()
        {
            Assert.True(
                new LiveNumber(
                    () => (5.243)
                ).AsDouble() == 5.243d
            );
        }

        [Fact]
        public void AveragesLiveNumbers()
        {
            var list = new List<int>() { 1, 2, 3 };
            var liveAverage = new LiveNumber(() => new AvgOf(list));
            var first = liveAverage.AsDouble();
            list.Add(4);
            Assert.NotEqual(first, liveAverage.AsDouble());
        }
    }
}
