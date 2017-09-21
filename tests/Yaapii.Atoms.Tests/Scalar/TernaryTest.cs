using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class TernaryTest
    {

        [Fact]
        public void ConditionTrue()
        {
            Assert.True(
                new Ternary<bool, int>(
                    new True(),
                    6,
                    16
                ).Value() == 6);
        }

        [Fact]
        public void ConditionFalse()
        {
            Assert.True(
                new Ternary<bool, int>(
                    new False(),
                    6,
                    16
                ).Value() == 16);
        }

        [Fact]
        public void ConditionBoolean()
        {
            Assert.True(
            new Ternary<bool, int>(
                true,
                6,
                16
            ).Value() == 6);
        }

        [Fact]
        public void ConditionFunc()
        {
            Assert.True(
                new Ternary<int, int>(
                    5,
                    input => input > 3,
                    input => input = 8,
                    input => input = 2
                ).Value() == 8);
        }
    }
}