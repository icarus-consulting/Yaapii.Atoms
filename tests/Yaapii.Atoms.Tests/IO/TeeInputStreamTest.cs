// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
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