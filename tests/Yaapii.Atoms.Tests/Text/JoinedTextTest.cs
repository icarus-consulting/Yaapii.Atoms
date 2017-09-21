using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class JoinedTextTest
    {
        [Fact]
        public void JoinsStrings()
        {
            Assert.True(
                new JoinedText(
                    " ", 
                    "hello", 
                    "world"
                ).AsString() == "hello world",
            "Can't join strings");
        }

        [Fact]
        public void JoinsTexts()
        {
            Assert.True(
                new JoinedText(
                    new TextOf(" "),
                    new TextOf("foo"),
                    new TextOf("bar")
                ).AsString() == "foo bar",
                "Can't join texts");
        }
    }
}