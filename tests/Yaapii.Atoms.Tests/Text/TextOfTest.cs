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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Yaapii.Atoms.Text.Tests
{
    public sealed class TextOfTest
    {

        [Fact]
        public void ReadsInputIntoText()
        {
            var content = "привет, друг!";

            Assert.True(
            new TextOf(
                new InputOf(content),
                Encoding.UTF8
            ).AsString() == content,
            "Can't read text from Input");
        }

        [Fact]
        public void ReadsInputIntoTextWithDefaultCharset()
        {
            var content = "Hello, друг! with default charset";
            Assert.True(
                new TextOf(
                    new InputOf(content)
                ).AsString() == content,
                "Can't read text from Input with default charset");
        }

        [Fact]
        public void ReadsInputIntoTextWithSmallBuffer()
        {
            var content = "Hi, товарищ! with small buffer";

            Assert.True(
                    new TextOf(
                        new InputOf(content),
                        2,
                        Encoding.UTF8
                    ).AsString() == content,
                    "Can't read text with a small reading buffer");
        }

        [Fact]
        public void ReadsInputIntoTextWithSmallBufferAndDefaultCharset()
        {
            var content = "Hello, товарищ! with default charset";

            Assert.True(
                    new TextOf(
                        new InputOf(content),
                        2
                    ).AsString() == content,
                    "Can't read text with a small reading buffer and default charset");
        }

        [Fact]
        public void ReadsFromReader()
        {
            String source = "hello, друг!";
            Assert.True(
            new TextOf(
                new StringReader(source),
                Encoding.UTF8
            ).AsString() == Encoding.UTF8.GetString(new BytesOf(source).AsBytes()),
            "Can't read string through a reader");
        }


        [Fact]
        public void ReadsFromReaderWithDefaultEncoding()
        {
            String source = "hello, друг! with default encoding";
            Assert.True(
                new TextOf(new StringReader(source)).AsString() ==
                    Encoding.UTF8.GetString(new BytesOf(source).AsBytes()),
                "Can't read string with default encoding through a reader");
        }

        [Fact]
        public void readsEncodedArrayOfCharsIntoText()
        {
            Assert.True(
                    new TextOf(
                        'O', ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a',
                        ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a'
                    ).AsString().CompareTo("O que sera que sera") == 0,
                    "Can't read array of encoded chars into text.");
        }

        [Fact]
        public void ReadsAnArrayOfBytes()
        {
            byte[] bytes = new byte[] { (byte)0xCA, (byte)0xFE };
            Assert.True(
                new TextOf(
                    bytes
                ).AsString().CompareTo(Encoding.UTF8.GetString(bytes)) == 0,
                "Can't read array of bytes");
        }

        [Fact]
        public void ComparesWithASubtext()
        {
            Assert.True(
            new TextOf("here to there").CompareTo(
                new SubText("from here to there", 5)
            ) == 0,
            "Can't compare sub texts");
        }

        [Fact]
        public void ReadsStringBuilder()
        {
            String starts = "Name it, ";
            String ends = "then it exists!";
            Assert.True(
                    new TextOf(
                        new StringBuilder(starts).Append(ends)
                    ).AsString() == starts + ends,
                    "Can't process a string builder");
        }

        [Fact]
        public void PrintsStackTrace()
        {
            Assert.True(
                new TextOf(
                    new IOException(
                        "It doesn't work at all"
                    )
                ).AsString().Contains("It doesn't work at all"),
                "Can't print exception stacktrace");
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum