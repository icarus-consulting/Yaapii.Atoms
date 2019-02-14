// MIT License
//
// Copyright(c) 2019 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    class ValidatedZip : IInput
    {
        private readonly StickyScalar<Stream> stream;

        public ValidatedZip(IInput input)
        {
            this.stream = new StickyScalar<Stream>(() =>
            {
                new FailPrecise(
                    new FailWhen(!IsZip(input.Stream())),
                    new InvalidOperationException($"Content is not compressed with either GZIP or PKZIP")
                ).Go();
                return input.Stream();
            });
            
        }
        public Stream Stream()
        {
            return this.stream.Value();
        }

        private bool IsPkZip(byte[] bytes)
        {
            var zipLeadBytes = 0x04034b50;
            bool isZip = false;
            if (bytes.Length > 4)
            {
                isZip = false;
            }
            else
            {
                isZip = (BitConverter.ToInt32(bytes, 0) == zipLeadBytes);
            }
            return isZip;
        }

        private bool IsGZip(byte[] bytes)
        {
            var gzipLeadBytes = 0x8b1f;
            bool isZip = false;
            if (bytes == null && bytes.Length >= 2)
            {
                isZip = false;
            }
            else
            {
                isZip = (BitConverter.ToUInt16(bytes, 0) == gzipLeadBytes);
            }
            return isZip;
        }

        private bool IsZip(Stream content)
        {
            byte[] bytes = new byte[4];
            content.Read(bytes, 0, 4);
            content.Position = 0;
            return IsPkZip(bytes) || IsGZip(bytes);
        }
    }
}
