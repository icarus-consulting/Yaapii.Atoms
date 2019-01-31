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
using System.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Bytes
{
    /// <summary>
    /// Bytes from Hex String
    /// </summary>
    public sealed class HexBytes : IBytes
    {
        private readonly IText origin;

        /// <summary>
        /// Bytes from Hex String
        /// </summary>
        /// <param name="origin">The string in Hex format</param>
        public HexBytes(string origin) : this(new TextOf(origin))
        { }
        /// <summary>
        /// Bytes from Hex String
        /// </summary>
        /// <param name="origin">The string in Hex format</param>
        public HexBytes(IText origin)
        {
            this.origin = origin;
        }
        public byte[] AsBytes()
        {
            var hex = this.origin.AsString();
            if ((hex.Length & 1) == 1)
            {
                throw new IOException("Length of hexadecimal text is odd");
            }
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }
    }
}
