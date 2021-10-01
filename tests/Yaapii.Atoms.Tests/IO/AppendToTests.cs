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

using System;
using System.IO;
using Xunit;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class AppendToTests
    {
        [Fact]
        public void WritesSimplePathContent()
        {
            var temp = Directory.CreateDirectory("artifacts/AppendToTest/");
            var file = Path.GetFullPath(Path.Combine(temp.FullName, "file.txt"));
            if (File.Exists(file)) File.Delete(file);

            var txt = "Hello, товарищ!";

            var lengthOf = new LengthOf(
                new TeeInput(
                    txt,
                    new AppendTo(
                        new OutputTo(file)
                    )));

            lengthOf.Value();
            lengthOf.Value();

            Assert.True(
                new LiveText(
                    new InputAsBytes(
                        new InputOf(new Uri(file))))
                .AsString() == (txt + txt),
                "Can't append path content");
        }

        [Fact]
        public void WritesSimpleFileContent()
        {
            var temp = Directory.CreateDirectory("artifacts/AppendToTest");
            var file = new Uri(Path.GetFullPath(Path.Combine(temp.FullName, "file.txt")));
            if (File.Exists(file.AbsolutePath))
            {
                File.Delete(file.AbsolutePath);
            }

            var txt = "Hello, друг!";
            var lengthOf = new LengthOf(
                new TeeInput(
                    txt,
                    new AppendTo(
                        new OutputTo(file))));

            lengthOf.Value();
            lengthOf.Value();

            Assert.True(
                new LiveText(
                    new InputAsBytes(
                        new InputOf(file)))
                .AsString() == txt + txt
            );
        }

        [Fact]
        public void DisposesStream()
        {
            var temp = Directory.CreateDirectory("artifacts/AppendToTest");
            var file = new Uri(Path.GetFullPath(Path.Combine(temp.FullName, "file.txt")));

            var appendTo = new AppendTo(file);

            var stream = appendTo.Stream();
            Assert.True(stream.CanWrite);
            stream.Dispose();
            Assert.False(stream.CanWrite);

            stream = appendTo.Stream();
            Assert.True(stream.CanWrite);
            appendTo.Dispose();
            Assert.False(stream.CanWrite);
        }
    }
}