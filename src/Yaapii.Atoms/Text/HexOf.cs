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

namespace Yaapii.Atoms.Texts
{
    /// <summary>
    /// Hexadecimal representation of Bytes.
    /// This object is sticky by default.
    /// </summary>
    public sealed class HexOf : Text.Envelope
    {
        private static readonly char[] HEX_CHARS = new char[] {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'
        };

        /// <summary>
        /// Hexadecimal representation of Bytes.
        /// </summary>
        /// <param name="bytes">bytes</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public HexOf(IBytes bytes) : base(() =>
            {
                var rawBytes = bytes.AsBytes();
                var hex = new char[rawBytes.Length * 2];
                var chr = -1;
                for (int i = 0; i < rawBytes.Length; i++)
                {
                    int value = 0xff & rawBytes[i];
                    hex[++chr] = HexOf.HEX_CHARS[value >> 4];
                    hex[++chr] = HexOf.HEX_CHARS[value & 0x0f];
                }
                return new string(hex);
            },
            false
        )
        { }
    }
}