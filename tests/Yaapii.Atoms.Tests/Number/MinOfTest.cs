using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Number;

namespace Yaapii.Atoms.Number.Tests
{
    public sealed class MinOfTest
    {
        [Fact]
        public void FindsMinOfIntsAsInt()
        {

            Assert.True(
                new MinOf(
                    1, 2, 3, 4
                ).AsInt() == 1);
        }

        [Fact]
        public void FindsMinOfIntsAsFloat()
        {

            Assert.True(
                new MinOf(
                    1, 2, 3, 4
                ).AsFloat() == 1F);
        }

        [Fact]
        public void FindsMinOfIntsAsLong()
        {

            Assert.True(
                new MinOf(
                    1, 2, 3, 4
                ).AsLong() == 1L);
        }

        [Fact]
        public void FindsMinOfIntsAsDouble()
        {

            Assert.True(
                new MinOf(
                    1, 2, 3, 4
                ).AsInt() == 1D);
        }

        [Fact]
        public void FindsMinOfFloatsAsInt()
        {

            Assert.True(
                new MinOf(
                    1.2F, 2.1F, 3.6F, 4.9F
                ).AsInt() == 1);
        }

        [Fact]
        public void FindsMinOfFloatsAsFloat()
        {

            Assert.True(
                new MinOf(
                    1.2F, 2.1F, 3.6F, 4.9F
                ).AsFloat() == 1.2F);
        }

        [Fact]
        public void FindsMinOfFloatsAsLong()
        {

            Assert.True(
                new MinOf(
                    1.2F, 2.1F, 3.6F, 4.9F
                ).AsLong() == 1L);
        }

        [Fact]
        public void FindsMinOfFloatsAsDouble()
        {

            Assert.True(
                new MinOf(
                    1.0D, 2.0D, 3.0D, 4.0D
                ).AsDouble() == 1.0D);
        }

        [Fact]
        public void FindsMinOfDoublesAsInt()
        {

            Assert.True(
                new MinOf(
                    1.2D, 2.1D, 3.6D, 4.9D
                ).AsInt() == 1);
        }

        [Fact]
        public void FindsMinOfDoublesAsFloat()
        {

            Assert.True(
                new MinOf(
                    1.2D, 2.1D, 3.6D, 4.9D
                ).AsFloat() == 1.2F);
        }

        [Fact]
        public void FindsMinOfDoublesAsLong()
        {

            Assert.True(
                new MinOf(
                    1.2D, 2.1D, 3.6D, 4.9D
                ).AsLong() == 1L);
        }

        [Fact]
        public void FindsMinOfDoublesAsDouble()
        {

            Assert.True(
                new MinOf(
                    1.0D, 2.0D, 3.0D, 4.0D
                ).AsDouble() == 1.0D);
        }

        [Fact]
        public void FindsMinOfLongsAsInt()
        {

            Assert.True(
                new MinOf(
                    1L, 2L, 3L, 4L
                ).AsInt() == 1);
        }

        [Fact]
        public void FindsMinOfLongsAsFloat()
        {

            Assert.True(
                new MinOf(
                    1L, 2L, 3L, 4L
                ).AsFloat() == 1.0F);
        }

        [Fact]
        public void FindsMinOfLongsAsLong()
        {

            Assert.True(
                new MinOf(
                    1L, 2L, 3L, 4L
                ).AsLong() == 1L);
        }

        [Fact]
        public void FindsMinOfLongsAsDouble()
        {

            Assert.True(
                new MinOf(
                    1L, 2L, 3L, 4L
                ).AsDouble() == 1.0D);
        }
    }
}