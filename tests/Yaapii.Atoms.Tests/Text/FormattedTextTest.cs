using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Yaapii.Atoms.Tests.Text
{
    public sealed class FormattedTextTest
    {
        [Fact]
        public void FormatsText()
        {
            Assert.True(
                new FormattedText(
                    "{0} Formatted {1}", 1, "text"
                ).AsString().Contains("1 Formatted text"),
                "Can't format a text");
        }

        [Fact]
        public void FormatsTextWithObjects()
        {
            Assert.True(
                new FormattedText(
                    new TextOf("{0}. Number as {1}"),
                    1,
                    "string"
                ).AsString().Contains("1. Number as string"),
                "Can't format a text with objects");
        }

        [Fact]
        public void FailsForInvalidPattern()
        {
            Assert.Throws(
                typeof(FormatException),
                () => new FormattedText(
                    new TextOf("Formatted { {0} }"),
                    new string[] {"invalid" }
            ).AsString());
        }

        [Fact]
        public void FormatsTextWithCollection()
        {
            Assert.True(
                new FormattedText(
                    new TextOf("{0}. Formatted as {1}"),
                    new String[] { "1", "txt" }
                ).AsString() == "1. Formatted as txt",
                "Can't format a text with a collection");
        }

        [Fact]
        public void FormatsWithLocale()
        {
            Assert.True(
                new FormattedText(
                    "{0:0.0}", new CultureInfo("de-DE"), 1234567890
                ).AsString() == "1234567890,0",
                "Can't format a text with Locale");
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
