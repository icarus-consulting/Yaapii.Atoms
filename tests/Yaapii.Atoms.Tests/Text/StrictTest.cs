using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Text.Tests
{
    public sealed class StrictTest
    {
        [Fact]
        public void Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                new Strict("not valid", "valid", "also valid").AsString()
            );
        }

        [Fact]
        public void ReturnsText()
        {
            var expected = "valid";
            Assert.Equal(
                expected,
                new Strict(expected, "not valid", "also not", "valid", "ending").AsString()
            );
        }

        [Fact]
        public void IgnoresCase()
        {
            var expected = "LargeValid";
            Assert.Equal(
                expected,
                new Strict(expected, "not valid", "also not", "LargeValid", "ending").AsString()
            );
        }

        [Fact]
        public void DoesNotIgnoresCase()
        {
            Assert.Throws<ArgumentException>(
                () =>
                new Strict("valid", false, "not valid", "also not", "largeValid", "ending").AsString()
            );
        }
    }
}
