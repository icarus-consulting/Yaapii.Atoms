﻿// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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
using System.IO.Compression;
using Xunit;
using Yaapii.Atoms.Bytes;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class GzipOutputTest
    {

        [Fact]
        public void WritesToGzipOutput()
        {
            byte[] bytes = {
                (byte) 0x1F, //GZIP Header ID1
                (byte) 0x8b, //GZIP Header ID2
                (byte) 0x08, (byte) 0x00, (byte) 0x00, (byte) 0x00, (byte) 0x00, (byte) 0x00, (byte) 0x00, //Header End
                (byte) 0x0B, (byte) 0xF2, (byte) 0x48, (byte) 0xCD, (byte) 0xC9, (byte) 0xC9, (byte) 0x57,
                (byte) 0x04, (byte) 0x00, (byte) 0x00, (byte) 0x00, (byte) 0xFF, (byte) 0xFF, (byte) 0x03,
                (byte) 0x00, (byte) 0x56, (byte) 0xCC, (byte) 0x2A, (byte) 0x9D, (byte) 0x06, (byte) 0x00,
                (byte) 0x00, (byte) 0x00
            };

            MemoryStream zipped = new MemoryStream();
            var zipStream = new GZipStream(zipped, CompressionLevel.Optimal);
            byte[] txt = new BytesOf("Hello!").AsBytes();

            new LengthOf(
                new TeeInput(
                    "Hello!",
                    new GZipOutput(new OutputTo(zipped))
                )
            ).Value();

            Assert.Equal(
                zipped.ToArray(),
                bytes
            );
        }

        [Fact]
        public void RejectsWhenClosed()
        {
            var path = Path.GetFullPath("assets/GZipOutput.txt");

            Assert.Throws<ArgumentException>(() =>
                {
                    var stream = new MemoryStream(new byte[256], true);
                    stream.Close();

                    new LengthOf(
                        new TeeInput(
                            "Hello!",
                            new GZipOutput(
                                new OutputTo(stream)
                            )
                        )
                    ).Value();
                }
            );
        }
    }

}
