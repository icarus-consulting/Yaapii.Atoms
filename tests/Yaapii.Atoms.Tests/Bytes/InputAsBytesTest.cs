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
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.IO.Tests;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Bytes.Tests
{
    public sealed class InputAsBytesTest
    {

        [Fact]
        public void ReadsLargeInMemoryContent()
        {
            int multiplier = 5_000;
            String body = "1234567890";
            Assert.True(
                new InputAsBytes(
                        new InputOf(
                        String.Join(
                            "",
                                new HeadOf<string>(
                                new Endless<string>(body),
                                multiplier
                            )
                        )
                    )
                ).AsBytes().Length == body.Length * multiplier,
                "Can't read large content from in-memory Input");
        }

        [Fact]
        public void ReadsLargeContent()
        {
            int size = 100_000;
            using (var slow = new SlowInputStream(size))
            {
                Assert.True(
                    new InputAsBytes(
                        new InputOf(slow)
                    ).AsBytes().Length == size,
                    "Can't read large content from Input");
            }
        }

        [Fact]
        public void ReadsInputIntoBytes()
        {
            var content = "Hello, друг!";

            Assert.True(
                    Encoding.UTF8.GetString(
                        new InputAsBytes(
                            new InputOf(
                                new BytesOf(
                                    new LiveText(content)
                                )
                            )
                        ).AsBytes()) == content,
                    "cannot read bytes into input");
        }

        [Fact]
        public void ReadsInputIntoBytesWithSmallBuffer()
        {
            var content = "Hello, товарищ!";

            Assert.True(
                    Encoding.UTF8.GetString(
                        new InputAsBytes(
                            new InputOf(
                                new BytesOf(
                                    new LiveText(content)
                                )
                            ),
                            2
                        ).AsBytes()) == content,
                    "cannot read bytes with small buffer");
        }

    }
}
