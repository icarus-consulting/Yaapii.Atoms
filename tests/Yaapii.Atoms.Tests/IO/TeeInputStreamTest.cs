using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;

#pragma warning disable NoStatics // No Statics"Can't copy InputStream to OutputStream byte by byte"
namespace Yaapii.Atoms.Tests.IO
{
    public sealed class TeeInputStreamTest
    {
        [Fact]
        public void CopiesContentByteByByte()
        {
            MemoryStream baos = new MemoryStream();
            String content = "Hello, товарищ!";

            Assert.True(
                TeeInputStreamTest.AsString(
                    new TeeInputStream(
                        new MemoryStream(
                            Encoding.UTF8.GetBytes(content)
                        ),
                        baos
                    )
                ) == Encoding.UTF8.GetString(baos.ToArray()),
            "Can't copy InputStream to OutputStream byte by byte");
        }


        private static String AsString(Stream input)
        {
            var baos = new MemoryStream();

            for (var i = 0; i < input.Length; i++)
            {
                baos.Write(new byte[1] { (Byte)input.ReadByte() }, 0, 1);
            }
            input.Dispose();

            return Encoding.UTF8.GetString(baos.ToArray());
        }
    }
}
#pragma warning restore NoStatics // No Statics