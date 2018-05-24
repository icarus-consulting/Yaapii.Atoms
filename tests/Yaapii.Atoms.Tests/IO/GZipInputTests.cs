using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class GZipInputTests
    {
        [Fact]
        public void Decompresses()
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

            Assert.Equal(
                "Hello!",
                new TextOf(
                    new GZipInput(new InputOf(bytes))
                ).AsString()
            );
        }
    }
}
