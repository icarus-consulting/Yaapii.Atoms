using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Number;

namespace Yaapii.Atoms.Tests.Number
{
    public sealed class AvgOfTest
    {
        [Fact]
        public void IsZeroForEmptyCollection()
        {
            Assert.True(
                new AvgOf(
                    new EnumerableOf<long>()
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
