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

using System;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class WriterAsOutputStreamTest
    {
        [Fact]
        public void WritesContentToFile()
        {
            var inputFile = new TempFile("large-text.txt");
            var outputFile = new TempFile("copy-text.txt");
            using (inputFile)
            {
                using (outputFile)
                {
                    var inputPath = Path.GetFullPath(inputFile.Value());
                    var outputPath = Path.GetFullPath(outputFile.Value());

                    //Create large file
                    new LengthOf(
                        new InputOf(
                            new TeeInputStream(
                                new MemoryStream(
                                    new BytesOf(
                                        new Text.Joined(",",
                                            new HeadOf<string>(
                                                new Endless<string>("Hello World"),
                                                1000
                                            )
                                        )
                                    ).AsBytes()
                                ),
                                new OutputTo(
                                    new Uri(inputPath)
                                ).Stream()
                            )
                        )
                    ).Value();

                    Assert.True(
                        new LengthOf(
                            new InputOf(
                                new TeeInputStream(
                                    new InputOf(
                                        inputPath
                                        ).Stream(),
                                    new WriterAsOutputStream(
                                        new StreamWriter(outputPath)
                                        )
                                    )
                                )
                            ).Value() ==
                        new LengthOf(
                            new InputOf(
                                new Uri(Path.GetFullPath(outputPath))
                                )
                            ).Value(),
                        "input and output are not the same size"
                        );
                }
            }
        }

        [Fact]
        public void RejectsReading()
        {
            Assert.ThrowsAny<NotImplementedException>(
                () => new WriterAsOutputStream(
                          new StreamWriter(
                              new MemoryStream()
                          )
                      ).Read(new byte[] {}, 0, 1)
            );
        }

        [Fact]
        public void RejectsSeeking()
        {
            Assert.ThrowsAny<NotImplementedException>(
                () => new WriterAsOutputStream(
                          new StreamWriter(
                              new MemoryStream()
                          )
                      ).Seek(3L, SeekOrigin.Begin)
            );
        }

        [Fact]
        public void RejectsSettingLength()
        {
            Assert.ThrowsAny<NotImplementedException>(
                () => new WriterAsOutputStream(
                          new StreamWriter(
                              new MemoryStream()
                          )
                      ).SetLength(5L)
            );
        }
    }
}

