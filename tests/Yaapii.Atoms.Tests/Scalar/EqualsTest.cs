using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class EqualsTest
    {
        [Fact]
        public void CompareEquals()
        {
            Assert.True(
                new Equals<int>(
                    () => 1,
                    () => 1
                ).Value() == true,
                "Can't compare if two integers are equals");
        }

        public void compareNotEquals()
        {
            Assert.True(
                new Equals<int>(
                    () => 1,
                    () => 2
                ).Value() == false);
        }

        [Fact]
        public void CompareEqualsText()
        {
            var str = "hello";
            Assert.True(
            new Equals<string>(
                () => str,
                () => str
            ).Value() == true,
            "Can't compare if two strings are equals");
        }

        [Fact]
        public void CompareNotEqualsText()
        {
            Assert.True(
            new Equals<string>(
                () => "world",
                () => "worle"
            ).Value() == false);
        }
    }
}
