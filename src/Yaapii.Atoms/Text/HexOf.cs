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

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// Hexadecimal representation of Bytes.
    /// </summary>
    public sealed class HexOf : IText
    {
        private static readonly char[] HEX_CHARS = new char[] {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'
        };

        private readonly IBytes _bytes;

        /// <summary>
        /// Hexadecimal representation of Bytes.
        /// </summary>
        /// <param name="bytes">bytes</param>
        public HexOf(IBytes bytes)
        {
            this._bytes = bytes;
        }

        /// <summary>
        /// Get hexadecimal representation of Bytes.
        /// </summary>
        /// <returns>Hexadecimal representation as a string</returns>
        public string AsString()
        {
            var bytes = this._bytes.AsBytes();
            var hex = new char[bytes.Length * 2];
            var chr = -1;
            for(int i=0; i < bytes.Length; i++)
            {
                int value = 0xff & bytes[i];
                hex[++chr] = HexOf.HEX_CHARS[value >> 4];
                hex[++chr] = HexOf.HEX_CHARS[value & 0x0f];
            }
            return new string (hex);
        }
    }
}