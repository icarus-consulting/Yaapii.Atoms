using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.IO;

namespace Yaapii.Atoms.Bytes.Tests
{
    public sealed class Base64BytesTest
    {
        [Fact]
        public void EncodesBase64()
        {
            Assert.True(
                new BytesEqual(
                    new Base64Bytes(
                        new BytesOf(
                            "SGVsbG8h")
                    ), 
                    new BytesOf(
                        "Hello!")
                ).Value()
            );
        }
    }
}
