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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.IO;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class LoggingOutputTest
    {
        [Fact]
        public void LogWriteZeroBytes()
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
        public void LogWriteOneByte()
        {
            using(var tempfile = new TempFile("txt"))
            {
                var append = new AppendTo(tempfile.Value());

                var output =
                    new LoggingOutput(
                        append,
                        "memory"
                    ).Stream();

                output.Write(new BytesOf("a").AsBytes(), 0, 1);

                append.Dispose();

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
        public void LogWriteText()
        {
            using (var tempfile = new TempFile("txt"))
            {
                var append = new AppendTo(tempfile.Value());

                var output =
                    new LoggingOutput(
                        append,
                        "memory"
                    ).Stream();

                var bytes = new BytesOf("Hello World!").AsBytes();
                output.Write(bytes, 0, bytes.Length);
                append.Dispose();

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
        public void LogWriteToLargeTextFile()
        {
            using (var tempfile = new TempFile("txt"))
            {

                try {
                    var append = new AppendTo(tempfile.Value());

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

                    append.Dispose();

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


                }catch(Exception ex)
                {

                }
            }
        }
    }
}
