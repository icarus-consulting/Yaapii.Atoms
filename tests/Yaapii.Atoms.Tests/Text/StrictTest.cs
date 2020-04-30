using System;
using Xunit;
using Yaapii.Atoms.Enumerable;

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

        [Fact]
        public void WorksWithList()
        {
            var expected = "TextWith!§$/()?`";
            Assert.Equal(
                expected,
                new Strict(expected,
                    new ManyOf("NotValid", expected)
                ).AsString()
            );
        }

        [Fact]
        public void IsStickyByDefault()
        {
            var expected = "expected";
            var counter = 0;
            var text =
                new Strict(new LiveText(() => expected),
                    new LiveMany<IText>(
                        new LiveText(() => counter++.ToString()),
                        new TextOf(expected)
                    )
                );
            text.AsString();
            text.AsString();
            Assert.Equal(
                1,
                counter
            );
        }

        [Fact]
        public void NotIgnoresCaseList()
        {
            var expected = "expected";
            Assert.Equal(
                expected,
                new Strict(new TextOf(expected), false,
                    new ManyOf<IText>(
                        new TextOf("Expected"),
                        new TextOf("Not Valid"),
                        new TextOf(expected)
                    )
                ).AsString()
            );
        }

        [Fact]
        public void IgnoresCaseList()
        {
            var expected = "expected";
            Assert.Equal(
                expected,
                new Strict(
                    expected, true,
                    new ManyOf(
                        "Not Valid",
                        "As well not valid",
                        "ExpEcteD"
                    )
                ).AsString()
            );
        }

        [Fact]
        public void IgnoresCaseByDefault()
        {
            var expected = "expected";
            Assert.Equal(
                expected,
                new Strict(
                    expected,
                    "Not Valid",
                    "As well not valid",
                    "ExpEcteD"
                ).AsString()
            );
        }
    }
}
