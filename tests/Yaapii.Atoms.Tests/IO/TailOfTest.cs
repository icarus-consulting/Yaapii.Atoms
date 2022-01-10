// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
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
using System.Linq;
using Xunit;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.Tests;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class TailOfTest
    {
        [Fact]
        public void TailsOnLongStream()
        {
            var size = 4;
            byte[] bytes = new RandomBytes(size).ToArray();

            var b =
                new BytesOf(
                    new TailOf(
                        new InputOf(
                            new BytesOf(bytes)
                        ),
                        size - 1
                    )
                ).AsBytes();

            var dest = new byte[bytes.Length - 1];
            Array.Copy(bytes, 1, dest, 0, bytes.Length - 1);

            Assert.Equal(
                b,
                dest
            );
        }

        [Fact]
        public void TailsOnExactStream()
        {
            int size = 4;
            byte[] bytes = new RandomBytes(size).ToArray();

            var b =
                new BytesOf(
                    new TailOf(
                        new InputOf(new BytesOf(bytes)),
                        size
                    )
                ).AsBytes();

            Assert.Equal(
                b,
                bytes
            );
        }

        [Fact]
        public void TailsOnExactStreamAndBuffer()
        {
            int size = 4;
            byte[] bytes = new RandomBytes(size).ToArray();

            Assert.Equal(
                new BytesOf(
                    new TailOf(
                        new InputOf(new BytesOf(bytes)),
                        size,
                        size
                    )
                ).AsBytes(),
                bytes
            );
        }

        [Fact]
        public void TailsOnShorterStream()
        {
            int size = 4;
            byte[] bytes = new RandomBytes(size).ToArray();

            Assert.Equal(
                new BytesOf(
                    new TailOf(
                        new InputOf(new BytesOf(bytes)),
                        size + 1
                    )
                ).AsBytes(),
                bytes
            );
        }

        [Fact]
        public void TailsOnStreamLongerThanBufferAndBytes()
        {
            int size = 4;
            byte[] bytes = new RandomBytes(size).ToArray();

            var res = new byte[bytes.Length - 1];
            Array.Copy(bytes, 1, res, 0, bytes.Length - 1);
            Assert.Equal(
                new BytesOf(
                    new TailOf(
                        new InputOf(new BytesOf(bytes)),
                        size - 1,
                        size - 1
                    )
                ).AsBytes(),
                res
            );
        }

        [Fact]
        public void failsIfBufferSizeSmallerThanTailSize()
        {
            int size = 4;
            var bytes = new RandomBytes(size).ToArray();
            Assert.Throws<ArgumentException>(
                () =>
                {
                    new BytesOf(
                        new TailOf(
                            new InputOf(
                                new BytesOf(bytes)
                            ),
                            size,
                            size - 1
                        )
                    ).AsBytes();
                });
        }
    }
}
