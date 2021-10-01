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

using Xunit;

namespace Yaapii.Atoms.Number.Tests
{
    public sealed class MaxOfTest
    {
        [Fact]
        public void FindsMaxOfIntsAsInt()
        {

            Assert.True(
                new MaxOf(
                    1, 2, 3, 4
                ).AsInt() == 4);
        }

        [Fact]
        public void FindsMaxOfIntsAsFloat()
        {

            Assert.True(
                new MaxOf(
                    1, 2, 3, 4
                ).AsFloat() == 4F);
        }

        [Fact]
        public void FindsMaxOfIntsAsLong()
        {

            Assert.True(
                new MaxOf(
                    1, 2, 3, 4
                ).AsLong() == 4L);
        }

        [Fact]
        public void FindsMaxOfIntsAsDouble()
        {

            Assert.True(
                new MaxOf(
                    1, 2, 3, 4
                ).AsInt() == 4D);
        }

        [Fact]
        public void FindsMaxOfFloatsAsInt()
        {

            Assert.True(
                new MaxOf(
                    1.2F, 2.1F, 3.6F, 4.9F
                ).AsInt() == 4);
        }

        [Fact]
        public void FindsMaxOfFloatsAsFloat()
        {

            Assert.True(
                new MaxOf(
                    1.2F, 2.1F, 3.6F, 4.9F
                ).AsFloat() == 4.9F);
        }

        [Fact]
        public void FindsMaxOfFloatsAsLong()
        {

            Assert.True(
                new MaxOf(
                    1.2F, 2.1F, 3.6F, 4.9F
                ).AsLong() == 4L);
        }

        [Fact]
        public void FindsMaxOfFloatsAsDouble()
        {

            Assert.True(
                new MaxOf(
                    1.0D, 2.0D, 3.0D, 4.0D
                ).AsDouble() == 4.0D);
        }

        [Fact]
        public void FindsMaxOfDoublesAsInt()
        {

            Assert.True(
                new MaxOf(
                    1.2D, 2.1D, 3.6D, 4.9D
                ).AsInt() == 4);
        }

        [Fact]
        public void FindsMaxOfDoublesAsFloat()
        {

            Assert.True(
                new MaxOf(
                    1.2D, 2.1D, 3.6D, 4.9D
                ).AsFloat() == 4.9F);
        }

        [Fact]
        public void FindsMaxOfDoublesAsLong()
        {

            Assert.True(
                new MaxOf(
                    1.2D, 2.1D, 3.6D, 4.9D
                ).AsLong() == 4L);
        }

        [Fact]
        public void FindsMaxOfDoublesAsDouble()
        {

            Assert.True(
                new MaxOf(
                    1.0D, 2.0D, 3.0D, 4.0D
                ).AsDouble() == 4.0D);
        }

        [Fact]
        public void FindsMaxOfLongsAsInt()
        {

            Assert.True(
                new MaxOf(
                    1L, 2L, 3L, 4L
                ).AsInt() == 4);
        }

        [Fact]
        public void FindsMaxOfLongsAsFloat()
        {

            Assert.True(
                new MaxOf(
                    1L, 2L, 3L, 4L
                ).AsFloat() == 4.0F);
        }

        [Fact]
        public void FindsMaxOfLongsAsLong()
        {

            Assert.True(
                new MaxOf(
                    1L, 2L, 3L, 4L
                ).AsLong() == 4L);
        }

        [Fact]
        public void FindsMaxOfLongsAsDouble()
        {

            Assert.True(
                new MaxOf(
                    1L, 2L, 3L, 4L
                ).AsDouble() == 4.0D);
        }
    }
}
