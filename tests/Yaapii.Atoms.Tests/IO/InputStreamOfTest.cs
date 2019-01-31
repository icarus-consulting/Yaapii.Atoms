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
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class InputStreamOfTest
    {
        [Fact]
        public void ReadsSimpleFileContent()
        {
            var dir = "artifacts/InputStreamOfTest"; var file = "txt-1"; var path = Path.GetFullPath(Path.Combine(dir, file));
            Directory.CreateDirectory(dir);
            if (File.Exists(path)) File.Delete(path);

            String content = "Hello, товарищ!";
            File.WriteAllBytes(path, new BytesOf(new TextOf(content, Encoding.UTF8)).AsBytes());

            Assert.True(
                new TextOf(
                    new InputAsBytes(
                        new InputOf(
                            new InputStreamOf(
                                new Uri(path))))
                ).AsString() == content,
                "Can't read file content");

        }

        [Fact]
        public void ReadsFromReader()
        {
            String content = "Hello, дорогой товарищ!";
            Assert.True(
                new TextOf(
                    new InputOf(
                        new InputStreamOf(
                            new StreamReader(
                                new InputOf(content).Stream())))
                ).AsString() == content);
        }

        [Fact]
        public void ReadsFromReaderThroughSmallBuffer()
        {
            String content = "Hello, صديق!";
            Assert.True(
                new TextOf(
                    new InputOf(
                        new InputStreamOf(
                            new StreamReader(
                                new InputOf(content).Stream()), 
                            1))).AsString() == content,
                "Can't read from reader through small buffer");
        }

        [Fact]
        public void MakesDataAvailable()
        {
            String content = "Hello,חבר!";
            Assert.True(
                new InputStreamOf(content).Length > 0,
                "Can't show that data is available");
        }

        [Fact]
        public void ReadsSimpleFileContentWithWhitespacesInUri()
        {
            var dir = "artifacts/Input StreamOf Test";
            var file = "txt-1";
            var path = Path.GetFullPath(Path.Combine(dir, file));

            Directory.CreateDirectory(dir);
            if (File.Exists(path)) File.Delete(path);

            String content = "Hello, товарищ!";
            File.WriteAllBytes(path, new BytesOf(new TextOf(content, Encoding.UTF8)).AsBytes());

            Assert.True(
                  new TextOf(
                      new InputAsBytes(
                          new InputOf(
                               new InputStreamOf(
                                    new Uri(path))))
                   ).AsString() == content,
                   "Can't read file content");
        }
    }
}
