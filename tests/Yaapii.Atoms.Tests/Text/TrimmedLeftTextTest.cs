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
    public sealed class TrimmedLeftTextTest
    {
        [Fact]
        public void ConvertsText()
        {
            Assert.True(
                new TrimmedLeftText(new TextOf("  Hello!   \t ")).AsString() == "Hello!   \t ",
                "Can't trim text");
        }

        [Fact]
        public void TrimmedBlankTextIsEmptyText()
        {
            Assert.True(
                new TrimmedLeftText(new TextOf("  \t ")).AsString() == "",
                "Can't trim a blank text");
        }

        [Fact]
        public void TrimsString()
        {
            Assert.True(
                new TrimmedLeftText(" \b  Hello!").AsString() == "Hello!"
            );
        }
        [Fact]
        public void TrimsIText()
        {
            Assert.True(
                new TrimmedLeftText(new TextOf(" \b  Hello! \b  ")).AsString() == "Hello! \b  "
            );
        }

        [Fact]
        public void TrimsStringWithString()
        {
            Assert.True(
                new TrimmedLeftText("Hello!", "Hell").AsString() == "o!"
            );
        }
        [Fact]
        public void TrimsStringWithCharArray()
        {
            Assert.True(
                new TrimmedLeftText(" \b   \t      Hello! \t \b  ", new char[] { '\b', '\t', ' ' }).AsString() == "Hello! \t \b  "
            );
        }
        [Fact]
        public void TrimsStringWithIText()
        {
            Assert.True(
                new TrimmedLeftText("Hello! ", new TextOf("Hell")).AsString() == "o! "
            );
        }

        [Fact]
        public void TrimsITextWithString()
        {
            Assert.True(
                new TrimmedLeftText(new TextOf(" \b   \t      Hello! \t \b  "), " \b   \t      ").AsString() == "Hello! \t \b  "
            );
        }
        [Fact]
        public void TrimsITextWithCharArray()
        {
            Assert.True(
                new TrimmedLeftText(new TextOf(" \b   \t      Hello! \t \b  "), new char[] { '\b', '\t', ' ' }).AsString() == "Hello! \t \b  "
            );
        }
        [Fact]
        public void TrimsITextWithIText()
        {
            Assert.True(
                new TrimmedLeftText(new TextOf(" \b   \t      Hello! \t \b  "), new TextOf(" \b   \t      ")).AsString() == "Hello! \t \b  "
            );
        }
    }
}
