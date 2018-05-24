// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
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

using Xunit;

namespace Yaapii.Atoms.Text.Tests
{
    public sealed class TrimmedRightTextTest
    {
        [Fact]
        public void convertsText()
        {
            Assert.True(
                new TrimmedRightText(new TextOf("  Hello!   \t ")).AsString() == "  Hello!",
                "Can't right trim a text");
        }

        [Fact]
        public void TrimmedBlankTextIsEmptyText()
        {
            Assert.True(
                new TrimmedRightText(new TextOf("  \t ")).AsString() == "",
                "Can't trim a blank text");
        }

        [Fact]
        public void TrimsString()
        {
            Assert.True(
                new TrimmedRightText(" \b  Hello! \b  ").AsString() == " \b  Hello!"
            );
        }
        [Fact]
        public void TrimsIText()
        {
            Assert.True(
                new TrimmedRightText(new TextOf(" \b  Hello! \b  ")).AsString() == " \b  Hello!"
            );
        }

        [Fact]
        public void TrimsStringWithString()
        {
            Assert.True(
                new TrimmedRightText("Hello!", "o!").AsString() == "Hell"
            );
        }
        [Fact]
        public void TrimsStringWithCharArray()
        {
            Assert.True(
                new TrimmedRightText(" \b   \t      Hello! \t \b  ", new char[] { '\b', '\t', ' ' }).AsString() == " \b   \t      Hello!"
            );
        }
        [Fact]
        public void TrimsStringWithIText()
        {
            Assert.True(
                new TrimmedRightText("  Hello! ", new TextOf("o! ")).AsString() == "  Hell"
            );
        }

        [Fact]
        public void TrimsITextWithString()
        {
            Assert.True(
                new TrimmedRightText(new TextOf(" \b   \t      Hello! \t \b  "), " \t \b  ").AsString() == " \b   \t      Hello!"
            );
        }
        [Fact]
        public void TrimsITextWithCharArray()
        {
            Assert.True(
                new TrimmedRightText(new TextOf(" \b   \t      Hello! \t \b  "), new char[] { '\b', '\t', ' ' }).AsString() == " \b   \t      Hello!"
            );
        }
        [Fact]
        public void TrimsITextWithIText()
        {
            Assert.True(
                new TrimmedRightText(new TextOf(" \b   \t      Hello! \t \b  "), new TextOf(" \t \b  ")).AsString() == " \b   \t      Hello!"
            );
        }
    }
}
