// MIT License
//
// Copyright(c) 2025 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Text.Tests
{
    public sealed class TrimmedRightTest
    {
        [Fact]
        public void TrimsWhitespaceEscapeSequences()
        {
            Assert.True(
                new TrimmedRight(
                    new LiveText("   \b \f \n \r \t \v   ")
                ).AsString() == string.Empty
            );
        }

        [Fact]
        public void TrimsString()
        {
            Assert.True(
                new TrimmedRight(" \b   \t      Hello! \t \b  ").AsString() == " \b   \t      Hello!"
            );
        }

        [Fact]
        public void TrimsText()
        {
            Assert.True(
                new TrimmedRight(
                    new LiveText(" \b   \t      Hello! \t \b  ")
                ).AsString() == " \b   \t      Hello!"
            );
        }

        [Fact]
        public void TrimsStringWithCharArray()
        {
            Assert.True(
                new TrimmedRight(
                    " \b   \t      Hello! \t \b  ",
                    new char[] { '\b', '\t', ' ', 'H', '!', 'o' }
                ).AsString() == " \b   \t      Hell"
            );
        }

        [Fact]
        public void TrimsTextWithCharArray()
        {
            Assert.True(
                new TrimmedRight(new LiveText(" \b   \t      Hello! \t \b  "), new char[] { '\b', '\t', ' ', 'H', '!', 'o' }).AsString() == " \b   \t      Hell"
            );
        }

        [Fact]
        public void TrimsTextWithScalar()
        {
            Assert.True(
                new TrimmedRight(
                    new LiveText(" \b   \t      Hello! \t \b  "),
                    new Live<char[]>(() => new char[] { '\b', '\t', ' ', 'H', '!', 'o' })
                ).AsString() == " \b   \t      Hell"
            );
        }

        [Fact]
        public void RemovesStringFromString()
        {
            Assert.True(
                new TrimmedRight(" \b   \t      Hello! \t \b   \t      H", " \b   \t      H").AsString() == " \b   \t      Hello! \t"
            );
        }

        [Fact]
        public void RemovesTextFromString()
        {
            Assert.True(
                new TrimmedRight(
                    new LiveText(" \b   \t      Hello! \t \b   \t      H"), " \b   \t      H").AsString() == " \b   \t      Hello! \t"
            );
        }

        [Fact]
        public void RemovesStringFromText()
        {
            Assert.True(
                new TrimmedRight(" \b   \t      Hello! \t \b   \t      H",
                    new LiveText(" \b   \t      H")
                ).AsString() == " \b   \t      Hello! \t"
            );
        }

        [Fact]
        public void RemovesTextFromText()
        {
            Assert.True(
                new TrimmedRight(
                    new LiveText(" \b   \t      Hello! \t \b   \t      H"),
                    new LiveText(" \b   \t      H")
                ).AsString() == " \b   \t      Hello! \t"
            );
        }
    }
}
