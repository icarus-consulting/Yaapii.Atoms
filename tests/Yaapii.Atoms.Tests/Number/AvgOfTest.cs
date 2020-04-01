// MIT License
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

using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Number.Tests
{
    public sealed class AvgOfTest
    {
        [Fact]
        public void IsZeroForEmptyCollection()
        {
            Assert.True(
                new AvgOf(
                    new ManyOf<long>()
                ).AsLong() == 0L);
        }

        [Fact]
        public void CalculatesAvgIntOfInts()
        {
            Assert.True(
                new AvgOf(1, 2, 3, 4).AsInt() == 2);
        }

        [Fact]
        public void CalculatesAvgLongOfInts()
        {
            Assert.True(
                new AvgOf(1, 2, 3, 4).AsLong() == 2L);
        }

        [Fact]
        public void CalculatesAvgDoubleOfInts()
        {
            Assert.True(
                new AvgOf(1, 2, 3, 4).AsDouble() == 2.5D);
        }

        [Fact]
        public void CalculatesAvgFloatOfInts()
        {
            Assert.True(
                new AvgOf(1, 2, 3, 4).AsFloat() == 2.5F);
        }

        [Fact]
        public void CalculatesAvgIntOfDoubles()
        {
            Assert.True(
                new AvgOf(1D, 2D, 3D, 4D).AsInt() == 2);
        }

        [Fact]
        public void CalculatesAvgLongOfDoubles()
        {
            Assert.True(
                new AvgOf(1D, 2D, 3D, 4D).AsLong() == 2L);
        }

        [Fact]
        public void CalculatesAvgDoubleOfDoubles()
        {
            Assert.True(
                new AvgOf(1D, 2D, 3D, 4D).AsDouble() == 2.5D);
        }

        [Fact]
        public void CalculatesAvgFloatOfDoubles()
        {
            Assert.True(
                new AvgOf(1D, 2D, 3D, 4D).AsFloat() == 2.5F);
        }

        [Fact]
        public void CalculatesAvgIntOfLongs()
        {
            Assert.True(
                new AvgOf(1L, 2L, 3L, 4L).AsInt() == 2);
        }

        [Fact]
        public void CalculatesAvgLongOfLongs()
        {
            Assert.True(
                new AvgOf(1L, 2L, 3L, 4L).AsLong() == 2L);
        }

        [Fact]
        public void CalculatesAvgDoubleOfLongs()
        {
            Assert.True(
                new AvgOf(1L, 2L, 3L, 4L).AsDouble() == 2.5D);
        }

        [Fact]
        public void CalculatesAvgFloatOfLongs()
        {
            Assert.True(
                new AvgOf(1L, 2L, 3L, 4L).AsFloat() == 2.5F);
        }

        [Fact]
        public void CalculatesAvgIntOfFloats()
        {
            Assert.True(
                new AvgOf(1F, 2F, 3F, 4F).AsInt() == 2);
        }

        [Fact]
        public void CalculatesAvgLongOfFloats()
        {
            Assert.True(
                new AvgOf(1F, 2F, 3F, 4F).AsLong() == 2L);
        }

        [Fact]
        public void CalculatesAvgDoubleOfFloats()
        {
            Assert.True(
                new AvgOf(1F, 2F, 3F, 4F).AsDouble() == 2.5D);
        }

        [Fact]
        public void CalculatesAvgFloatOfFloats()
        {
            Assert.True(
                new AvgOf(1F, 2F, 3F, 4F).AsFloat() == 2.5F);
        }
    }
}
