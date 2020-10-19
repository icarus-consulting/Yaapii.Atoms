using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Number;

namespace Yaapii.Atoms.Number.Tests
{
    public sealed class SimilarTest
    {
        [Fact]
        public void IsSimilarSame()
        {
            INumber first =
                new NumberOf(13.37);
            INumber second =
                new NumberOf(13.37);

            Assert.True(
                new Similar(first, second).Value()
                );
        }

        [Fact]
        public void IsSimilarNoDecimal()
        {
            INumber first =
                new NumberOf(13);
            INumber second =
                new NumberOf(13);

            Assert.True(
                new Similar(first, second, 10).Value()
                );
        }

        [Fact]
        public void IsSimilarOneDecimal()
        {
            INumber first =
                new NumberOf(13.37);
            INumber second =
                new NumberOf(13.3);

            Assert.True(
                new Similar(first, second, 1).Value()
                );
        }

        [Fact]
        public void IsSimilarOnlyOneDecimal()
        {
            INumber first =
                new NumberOf(13.37777);
            INumber second =
                new NumberOf(13.33333);

            Assert.True(
                new Similar(first, second, 1).Value()
                );
        }

        [Fact]
        public void IsSimilarFiveDecimal()
        {
            INumber first =
                new NumberOf(13.333337);
            INumber second =
                new NumberOf(13.333339);

            Assert.True(
                new Similar(first, second, 5).Value()
                );
        }

        [Fact]
        public void IsSimilar10Decimal()
        {
            INumber first =
                new NumberOf(13.33333333337);
            INumber second =
                new NumberOf(13.33333333339);

            Assert.True(
                new Similar(first, second, 10).Value()
                );
        }

        [Fact]
        public void IsNotSimilar()
        {
            INumber first =
                new NumberOf(13.37);
            INumber second =
                new NumberOf(13.39);

            Assert.False(
                new Similar(first, second, 2).Value()
                );
        }
    }
}