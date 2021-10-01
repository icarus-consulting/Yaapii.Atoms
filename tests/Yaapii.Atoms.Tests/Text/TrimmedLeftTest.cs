// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
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
    public sealed class TrimmedLeftTest
    {
        [Fact]
        public void TrimsWhitespaceEscapeSequences()
        {
            Assert.True(
                new TrimmedLeft(new LiveText("   \b \f \n \r \t \v   ")).AsString() == string.Empty
            );
        }

        [Fact]
        public void TrimsString()
        {
            Assert.Equal(
                "Hello! \t \b  ",
                new TrimmedLeft(
                    " \b   \t      Hello! \t \b  "
                ).AsString()
            );
        }

        [Fact]
        public void TrimsText()
        {
            Assert.Equal(
                "Hello! \t \b  ",
                new TrimmedLeft(
                    new LiveText(" \b   \t      Hello! \t \b  ")
                ).AsString()
            );
        }

        [Fact]
        public void TrimsStringWithCharArray()
        {
            Assert.Equal(
                "ello! \t \b  ",
                new TrimmedLeft(
                    " \b   \t      Hello! \t \b  ",
                    new char[] { '\b', '\t', ' ', 'H', 'o' }
                ).AsString()
            );
        }

        [Fact]
        public void TrimsTextWithCharArray()
        {
            Assert.Equal(
                "ello! \t \b  ",
                new TrimmedLeft(
                    new LiveText(" \b   \t      Hello! \t \b  "),
                    new char[] { '\b', '\t', ' ', 'H', 'o' }
                ).AsString()
            );
        }

        [Fact]
        public void TrimsTextWithScalar()
        {
            Assert.Equal(
                "ello! \t \b  ",
                new TrimmedLeft(
                    new LiveText(" \b   \t      Hello! \t \b  "),
                    new Live<char[]>(() => new char[] { '\b', '\t', ' ', 'H', 'o' })
                ).AsString()
            );
        }

        [Fact]
        public void RemovesStringFromString()
        {
            Assert.Equal(
                "ello! \t \b   \t      H",
                new TrimmedLeft(
                    " \b   \t      Hello! \t \b   \t      H", " \b   \t      H"
                ).AsString()
            );
        }

        [Fact]
        public void RemovesTextFromString()
        {
            Assert.Equal(
                "ello! \t \b   \t      H",
                new TrimmedLeft(new LiveText(" \b   \t      Hello! \t \b   \t      H"), " \b   \t      H").AsString()
            );
        }

        [Fact]
        public void RemovesStringFromText()
        {
            Assert.Equal(
                "ello! \t \b   \t      H",
                new TrimmedLeft(" \b   \t      Hello! \t \b   \t      H", new LiveText(" \b   \t      H")).AsString()
            );
        }

        [Fact]
        public void RemovesTextFromText()
        {
            Assert.Equal(
                "ello! \t \b   \t      H",
                new TrimmedLeft(
                    new LiveText(" \b   \t      Hello! \t \b   \t      H"),
                    new LiveText(" \b   \t      H")
                ).AsString()
            );
        }
    }
}
