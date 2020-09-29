using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class TailOfTest
    {
        [Fact]
        public void TailsOnLongStream()
        {
            var size = 4;
            byte[] bytes = this.Generate(size);

            var b =
                new BytesOf(
                    new TailOf(
                        new InputOf(
                            new BytesOf(bytes)
                        ),
                        size - 1
                    )
                ).AsBytes();

            var dest = new byte[bytes.Length-1];
            Array.Copy(bytes,1,dest,0, bytes.Length-1);
            
            Assert.Equal(
                b,
                dest
            );
        }

        [Fact]
        public void TailsOnExactStream()
        {
            int size = 4;
            byte[] bytes = this.Generate(size);

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
            byte[] bytes = this.Generate(size);
        
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
            byte[] bytes = this.Generate(size);
            
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
            byte[] bytes = this.Generate(size);

            var res = new byte[bytes.Length-1];
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
            var bytes = this.Generate(size);
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

        /**
         * Generate random byte array.
         * @param size Size of array
         * @return Generated array
         */
        private byte[] Generate(int size)
        {
            byte[] bytes = new byte[size];
            new Random().NextBytes(bytes);
            return bytes;
        }
    }
}
