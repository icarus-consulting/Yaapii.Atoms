using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Xunit;
using Yaapii.Atoms.Tests;

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
