using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;

namespace Yaapii.Atoms.Bytes
{
    public sealed class BytesBase64Test
    {
        [Fact]
        public void EncodesBase64()
        {
            Assert.True(
                new BytesEqual(
                    new BytesBase64(
                        new BytesOf(
                            "Hello!")
                    ),
                    new BytesOf(
                        "SGVsbG8h")
                ).Value()
            );
        }
    }
}
