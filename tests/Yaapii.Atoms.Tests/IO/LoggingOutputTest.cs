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

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class LoggingOutputTest
    {
        [Fact]
        public void LogsZeroBytesOnEmptyInput()
        {
            var res =
                new LengthOf(
                    new TeeInput(
                        new InputOf(""),
                        new LoggingOutput(
                            new ConsoleOutput(),
                            "memory"
                        )
                    )
                ).Value();

            Assert.Equal(
                0L,
                res
            );
        }

        [Fact]
        public void LogsWriteOneBytesToTextFile()
        {
            using (var tempfile = new TempFile("txt"))
            {
                using (var append = new AppendTo(tempfile.Value()))
                {
                    var output =
                        new LoggingOutput(
                            append,
                            "memory"
                        ).Stream();

                    output.Write(new BytesOf("a").AsBytes(), 0, 1);

                }
                var inputStream = new InputOf(new Uri(tempfile.Value())).Stream();
                var content = "";
                using (var reader = new StreamReader(inputStream))
                {
                    content = reader.ReadToEnd();
                }
                Assert.Equal(
                    "a",
                    content
                );
            }
        }

        [Fact]
        public void LogsWriteTextToTextFile()
        {
            using (var tempfile = new TempFile("txt"))
            {
                var bytes = new BytesOf("Hello World!").AsBytes();

                using (var append = new AppendTo(tempfile.Value()))
                {
                    var output =
                    new LoggingOutput(
                        append,
                        "memory"
                    ).Stream();


                    output.Write(bytes, 0, bytes.Length);
                }

                var inputStream = new InputOf(new Uri(tempfile.Value())).Stream();
                var content = "";
                using (var reader = new StreamReader(inputStream))
                {
                    content = reader.ReadToEnd();
                }

                Assert.Equal(
                    bytes,
                    new BytesOf(content).AsBytes()
                );
            }
        }

        [Fact]
        public void LogsWriteLargeTextToTextFile()
        {
            using (var tempfile = new TempFile("txt"))
            {

                using (var append = new AppendTo(tempfile.Value()))
                {
                    var output =
                    new LoggingOutput(
                        append,
                        "text file"
                    ).Stream();

                    var length =
                        new LengthOf(
                            new TeeInput(
                                new ResourceOf("Assets/Txt/large-text.txt", this.GetType()),
                                new OutputTo(output)
                            )
                        ).Value();

                }

                var inputStream = new InputOf(new Uri(tempfile.Value())).Stream();
                var content = "";
                var input = "";
                using (var reader = new StreamReader(inputStream))
                {
                    content = reader.ReadToEnd();
                }
                using (var reader = new StreamReader(new ResourceOf("Assets/Txt/large-text.txt", this.GetType()).Stream()))
                {
                    input = reader.ReadToEnd();
                }

                Assert.Equal(
                    input,
                    content
                );
            }
        }
    }
}
