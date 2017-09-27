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
using System.Collections;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

#pragma warning disable MaxPublicMethodCount
namespace Yaapii.Atoms.Tests.IO
{
    public sealed class InputOfTest
    {

        [Fact]
        public void ReadsAlternativeInputForFileCase()
        {
            Assert.True(
                new TextOf(
                    new InputWithFallback(
                        new InputOf(
                            new Uri(Path.GetFullPath("/this-file-does-not-exist.txt"))
                        ),
                        new InputOf(new TextOf("Alternative text!"))
                    )
                ).AsString().EndsWith("text!"),
                "Can't read alternative source from file not found");
        }

        [Fact]
        public void ReadsSimpleFileContent()
        {
            var dir = @"artifacts/InputOfTest"; var file = "simple-filecontent.txt"; var path = Path.GetFullPath(Path.Combine(dir, file));
            Directory.CreateDirectory(dir);
            String content = "Hello, товарищ!";

            new LengthOf(
                    new InputOf(
                        new TeeInputStream(
                            new MemoryStream(
                                new BytesOf(
                                    new JoinedText("\r\n",
                                    new Limited<string>(
                                        new Endless<string>(content),
                                        10))
                                    ).AsBytes()),
                            new OutputTo(
                                new Uri(path)).Stream()))).Value();

            Assert.True(
                    new TextOf(
                        new InputOf(
                            new Uri(path))
                    ).AsString().EndsWith(content),
                    "Can't read file content");
        }

        [Fact]
        public void ClosesInputStream()
        {
            Stream input;
            using (input = new MemoryStream(Encoding.UTF8.GetBytes("how are you?")))
            {
                new TextOf(
                    new InputOf(
                        input)).AsString();
            }

            Assert.False(input.CanRead,
                "cannot close input stream");
        }

        [Fact]
        public void ReadsFileContent()
        {
            var dir = "artifacts/InputOfTest"; var file = "small-text.txt"; var path = Path.GetFullPath(Path.Combine(dir, file));
            Directory.CreateDirectory(dir);
            if (File.Exists(path)) File.Delete(path);

            new LengthOf(
                new InputOf(
                    new TeeInputStream(
                        new MemoryStream(
                            new BytesOf(
                                new JoinedText("\r\n",
                                new Limited<string>(
                                    new Endless<string>("Hello World"),
                                    10))
                                ).AsBytes()),
                        new OutputTo(
                            new Uri(path)).Stream()))).Value();


            Assert.True(
                new TextOf(
                    new BytesOf(
                        new InputOf(
                            new Uri(Path.GetFullPath(path))
                            )
                        ).AsBytes()).AsString().StartsWith("Hello World"));
        }

        [Fact]
        public void ReadsRealUrl()
        {
            Assert.True(
                    new TextOf(
                        new InputOf(
                            new Url("http://www.google.de"))
                    ).AsString().Contains("<html"),
                    "Can't fetch bytes from the URL"
            );
        }

        [Fact]
        public void ReadsFile()
        {
            var dir = "artifacts/InputOfTest"; var file = "large-text.txt"; var path = Path.GetFullPath(Path.Combine(dir, file));
            Directory.CreateDirectory(dir);
            if (File.Exists(path)) File.Delete(path);

            var length = new LengthOf(
                new InputOf(
                    new TeeInputStream(
                        new MemoryStream(
                            new BytesOf(
                                new JoinedText("\r\n",
                                new Limited<string>(
                                    new Endless<string>("Hello World"),
                                    1000))
                                ).AsBytes()),
                        new OutputTo(
                            new Uri(path)).Stream()))
            ).Value();

            Assert.True(
                new LengthOf<string>(
                    new SplitText(
                        new TextOf(
                            new BytesOf(
                                new InputOf(
                                    new Uri(path)
                                )
                            )
                        ), "\r\n")
                ).Value() == 1000);
        }

        [Fact]
        public void ReadsStringIntoBytes()
        {
            var content = "Hello, друг!";

            Assert.True(
                    Encoding.UTF8.GetString(
                        new BytesOf(
                            new InputOf(content)
                        ).AsBytes()) == content,
                    "Can't read bytes from Input");
        }

        [Fact]
        public void ReadsStringBuilder()
        {
            String starts = "Name it, ";
            String ends = "then it exists!";
            Assert.True(
                    new TextOf(
                        new BytesOf(
                            new InputOf(
                                new StringBuilder(starts).Append(ends)
                            )
                        ).AsBytes()).AsString() == starts + ends,
                    "can't read from stringbuilder"
                );
        }

        [Fact]
        public void ReadsArrayOfChars()
        {
            Assert.True(
                    new TextOf(
                        new BytesOf(
                            new InputOf(
                                'H', 'o', 'l', 'd', ' ',
                                'i', 'n', 'f', 'i', 'n', 'i', 't', 'y'
                            )
                        ).AsBytes()).AsString() == "Hold infinity",
                    "Can't read array of chars.");
        }

        [Fact]
        public void ReadsEncodedArrayOfChars()
        {
            Assert.True(
                    new TextOf(
                        new BytesOf(
                            new InputOf(
                                new char[]{
                            'O', ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a',
                            ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a',
                                }
                            )
                        ).AsBytes()).AsString() == "O que sera que sera",
                        "Can't read array of encoded chars.");
        }

        [Fact]
        public void ReadsStringFromReader()
        {
            String source = "hello, source!";
            Assert.True(
            new TextOf(
                new InputOf(
                    new StreamReader(
                        new InputOf(source).Stream())
                )
            ).AsString() == source,
            "Can't read string through a reader");
        }

        [Fact]
        public void ReadsEncodedStringFromReader()
        {
            String source = "hello, друг!";
            Assert.True(
                new TextOf(
                    new InputOf(
                            new StreamReader(
                                new InputOf(source).Stream()),
                            Encoding.UTF8)
                ).AsString() == source,
                "Can't read encoded string through a reader");
        }

        [Fact]
        public void ReadsAnArrayOfBytes()
        {

            byte[] bytes = new byte[] { (byte)0xCA, (byte)0xFE };
            Assert.True(
                StructuralComparisons.StructuralEqualityComparer.Equals(
                new InputAsBytes(
                    new InputOf(bytes)
                ).AsBytes(), bytes),
                "Can't read array of bytes");
        }

        [Fact]
        public void MakesDataAvailable()
        {
            String content = "Hello,חבר!";
            Assert.True(
                    new InputOf(content).Stream().Length > 0,
                    "Can't show that data is available");
        }

    }
}