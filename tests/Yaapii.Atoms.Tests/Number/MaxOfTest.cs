using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Number;

namespace Yaapii.Atoms.Tests.Number
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
