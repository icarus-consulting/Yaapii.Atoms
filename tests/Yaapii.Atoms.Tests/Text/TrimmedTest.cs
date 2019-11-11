// MIT License
//
// Copyright(c) 2019 ICARUS Consulting GmbH
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
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Texts.Tests
{
    public sealed class TrimmedTest
    {
        [Fact]
        public void TrimsWhitespaceEscapeSequences()
        {
            Assert.True(
                new Trimmed(new TextOf("   \b \f \n \r \t \v   ")).AsString() == string.Empty
            );
        }

        [Fact]
        public void TrimsString()
        {
            Assert.True(
                new Trimmed(" \b   \t      Hello! \t \b  ").AsString() == "Hello!"
            );
        }

        [Fact]
        public void TrimsText()
        {
            Assert.True(
                new Trimmed(new TextOf(" \b   \t      Hello! \t \b  ")).AsString() == "Hello!"
            );
        }

        [Fact]
        public void TrimsStringWithCharArray()
        {
            Assert.True(
                new Trimmed(" \b   \t      Hello! \t \b  ", new char[] { '\b', '\t', ' ', 'H', 'o' }).AsString() == "ello!"
            );
        }

        [Fact]
        public void TrimsTextWithCharArray()
        {
            Assert.True(
                new Trimmed(new TextOf(" \b   \t      Hello! \t \b  "), new char[] { '\b', '\t', ' ', 'H', 'o' }).AsString() == "ello!"
            );
        }

        [Fact]
        public void TrimsTextWithScalar()
        {
            Assert.True(
                new Trimmed(new TextOf(" \b   \t      Hello! \t \b  "), new ScalarOf<char[]>(() => new char[] { '\b', '\t', ' ', 'H', 'o' })).AsString() == "ello!"
            );
        }

        [Fact]
        public void RemovesStringFromString()
        {
            Assert.True(
                new Trimmed(" \b   \t      Hello! \t \b   \t      H", " \b   \t      H").AsString() == "ello! \t"
            );
        }

        [Fact]
        public void RemovesTextFromString()
        {
            Assert.True(
                new Trimmed(new TextOf(" \b   \t      Hello! \t \b   \t      H"), " \b   \t      H").AsString() == "ello! \t"
            );
        }

        [Fact]
        public void RemovesStringFromText()
        {
            Assert.True(
                new Trimmed(" \b   \t      Hello! \t \b   \t      H", new TextOf(" \b   \t      H")).AsString() == "ello! \t"
            );
        }

        [Fact]
        public void RemovesTextFromText()
        {
            Assert.True(
                new Trimmed(new TextOf(" \b   \t      Hello! \t \b   \t      H"), new TextOf(" \b   \t      H")).AsString() == "ello! \t"
            );
        }

        [Fact]
        public void RemovesTextFromScalar()
        {
            Assert.True(
                new Trimmed(new TextOf(" \b   \t      Hello! \t \b   \t      H"), new ScalarOf<IText>(() => new TextOf(" \b   \t      H"))).AsString() == "ello! \t"
            );
        }
    }
}
