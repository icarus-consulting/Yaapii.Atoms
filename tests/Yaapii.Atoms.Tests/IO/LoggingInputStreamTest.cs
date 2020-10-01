using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Bytes;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class LoggingInputStreamTest
    {
        [Fact]
        void ReadEmptyStream()
        {
            var stream = 
                new LoggingInputStream(
                    new MemoryStream(
                        new BytesOf("").AsBytes()
                    ),
                    ""
                );
            Assert.Equal(
                0,
                stream.ReadByte()
            );
        }

        [Fact]
        void ReadByteByByte()
        {
            var stream = new LoggingInputStream(
                new MemoryStream(
                    new byte[] {
                            // @checkstyle MagicNumberCheck (2 lines)
                            (byte) 20,
                            (byte) 10,
                    }
                ),
                "ReadByteByByte"
            );

            Assert.Equal(
                20,
                stream.ReadByte()
            );

            Assert.Equal(
                10,
                stream.ReadByte()
            );
            Assert.Equal(
                0,
                stream.ReadByte()
            );
        }

        [Fact]
        void SkipFirstByte()
        {
            var stream = new LoggingInputStream(
                new MemoryStream(
                    new byte[] {
                            // @checkstyle MagicNumberCheck (2 lines)
                            (byte) 20,
                            (byte) 10,
                    }
                ),
                "ReadByteByByte"
            );

            stream.Seek(1, SeekOrigin.Begin);

            Assert.Equal(
                10,
                stream.ReadByte()
            );
            Assert.Equal(
                0,
                stream.ReadByte()
            );
        }
    }
}
