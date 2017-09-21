using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class ReplacedTextTest
    {
        [Fact]
        public void ReplaceText()
        {
            Assert.True(
                new ReplacedText(
                    new TextOf("Hello!"),
                    "ello", "i"
                ).AsString() == "Hi!",
                "Can't replace a text");
        }

        [Fact]
        public void NotReplaceTextWhenSubstringNotFound()
        {
            String text = "HelloAgain!";
            Assert.True(
                new ReplacedText(
                    new TextOf(text),
                    "xyz", "i"
                ).AsString() == text,
                "Replace a text abnormally");
        }

        [Fact]
        public void ReplacesAllOccurrences()
        {
            Assert.True(
                new ReplacedText(
                    new TextOf("one cat, two cats, three cats"),
                    "cat",
                    "dog"
                ).AsString() == "one dog, two dogs, three dogs",
                "Can't replace a text with multiple needle occurrences");
        }
    }
}
