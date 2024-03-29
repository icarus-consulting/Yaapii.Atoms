// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Globalization;
using Xunit;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Yaapii.Atoms.Text.Tests
{
    public sealed class FormattedTest
    {
        [Fact]
        public void FormatsText()
        {
            Assert.True(
                new Formatted(
                    "{0} Formatted {1}", 1, "text"
                ).AsString().Contains("1 Formatted text"),
                "Can't format a text");
        }

        [Fact]
        public void FormatsTextWithObjects()
        {
            Assert.True(
                new Formatted(
                    new LiveText("{0}. Number as {1}"),
                    1,
                    "string"
                ).AsString().Contains("1. Number as string"),
                "Can't format a text with objects");
        }

        [Fact]
        public void FailsForInvalidPattern()
        {
            Assert.Throws<FormatException>(
                () => new Formatted(
                    new LiveText("Formatted { {0} }"),
                    new string[] { "invalid" }
            ).AsString());
        }

        [Fact]
        public void FormatsTextWithCollection()
        {
            Assert.True(
                new Formatted(
                    new TextOf("{0}. Formatted as {1}"),
                    new String[] { "1", "txt" }
                ).AsString() == "1. Formatted as txt"
            );
        }

        [Fact]
        public void FormatsWithLocale()
        {
            Assert.True(
                new Formatted(
                    "{0:0.0}", new CultureInfo("de-DE"), 1234567890
                ).AsString() == "1234567890,0",
                "Can't format a text with Locale");
        }

        [Fact]
        public void FormatsWithstringAndText()
        {
            Assert.Equal(
                "This is a FormattedText test",
                new Formatted(
                    "{0} is a {1} test",
                    new LiveText("This"),
                    new LiveText("FormattedText")
                ).AsString()
            );

        }
    }
}
