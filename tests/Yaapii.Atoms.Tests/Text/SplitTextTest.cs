using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class SplitTextTest
    {
        [Fact]
        public void SplitText()
        {
            Assert.True(
            new SplitText(
                "Hello world!", "\\s+"
            ).Select(s => s == "Hello" || s == "world!").Count() == 2,
            "Can't split a text");
        }

        [Fact]
        public void SplitEmptyText()
        {
            Assert.True(
                new LengthOf<string>(
                    new SplitText("", "\n")).Value() == 0,
                    "Can't split an empty text");
        }

        [Fact]
        public void SplitStringWithTextRegex()
        {
            Assert.True(
                new SplitText(
                    "Atoms OOP!",
                    new TextOf("\\s")
                ).Select(s => s == "Atoms" || s == "OOP!").Count() == 2,
                "Can't split an string with text regex");
        }

        [Fact]
        public void SplitTextWithStringRegex()
        {
            Assert.True(
            new SplitText(
                new TextOf("Atoms4Primitives!"), "\\d+")
                .Select(s => s == "Atoms" || s == "Primitives!").Count() == 2,
            "Can't split an text with string regex");
        }

        [Fact]
        public void SplitTextWithTextRegex()
        {
            Assert.True(
                new SplitText(new TextOf("Split#OOP!"), "\\W+")
                .Select(s => s == "Split" || s == "OOP").Count() == 2,
                "Can't split an text with text regex");
        }

    }
}
