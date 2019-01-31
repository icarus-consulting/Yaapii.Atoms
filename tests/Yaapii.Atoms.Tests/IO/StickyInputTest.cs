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
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class StickyInputTest
    {
        [Fact]
        public void ReadsFileContent()
        {
            var dir = "artifacts/StickyInputTest"; var file = "large-text.txt"; var path = Path.GetFullPath(Path.Combine(dir, file));
            Directory.CreateDirectory(dir);
            if (File.Exists(path)) File.Delete(path);

            var str = "Hello World"; var lmt = "\r\n"; var times = 1000;

            var length = 
                new IO.LengthOf(
                    new InputOf(
                        new TeeInputStream(
                            new MemoryStream(
                                new BytesOf(
                                    new JoinedText(lmt,
                                    new Limited<string>(
                                        new Endless<string>(str),
                                        times))
                                    ).AsBytes()),
                            new OutputTo(
                                new Uri(path)).Stream()))
                ).Value();

            var ipt = new StickyInput(new InputOf(new Uri(path)));

            Assert.True(
                new RepeatedFunc<IInput, Boolean>(
                    input => new BytesOf(input).AsBytes().Length == length,
                    10
                ).Invoke(ipt),
                "can't return the same result every time");
        }

        [Fact]
        public void ReadsRealUrl()
        {
            Assert.True(
                new TextOf(
                        new StickyInput(
                            new InputOf(
                                new Url("http://www.google.de")))
                ).AsString().Contains("<html"),
            "Can't fetch text page from the URL");
        }

        [Fact]
        public void ReadsFileContentSlowlyAndCountsLength()
        {
            long size = 100_000L;
            Assert.True(
                new LengthOf(
                    new StickyInput(
                        new SlowInput(size)
                    )
                ).Value() == size,
                "Can't read bytes from a large source slowly and count length");
        }

        [Fact]
        public void ReadsFileContentSlowly()
        {
            int size = 130_000;
            Assert.True(
                new BytesOf(
                    new StickyInput(
                        new SlowInput(size)
                    )
                ).AsBytes().Length == size,
                "Can't read bytes from a large source slowly");
        }
    }
}
