using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Number;

namespace Yaapii.Atoms.Number.Tests
{
    public sealed class SumOfTest
    {
        [Fact]
        public void CalculatesSumOfNumbers()
        {
            Assert.True(
                new SumOf(
                    1.5F, 2.5F, 3.5F
                ).AsFloat() == 7.5F);
        }

        [Fact]
        public void CalculatesSumOfEnumerables()
        {
            Assert.True(
                new SumOf(
                    new EnumerableOf<float>(
                        1.5F, 2.5F, 3.5F
                    )
                ).AsFloat() == 7.5F);
        }
    }
}
