// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> whose characters have been rotated.
    /// </summary>
    public sealed class Rotated : TextEnvelope
    {
        /// <summary>
        /// A <see cref="IText"/> whose characters have been rotated.
        /// </summary>
        /// <param name="text">text to rotate</param>
        /// <param name="shift">direction and amount of chars to rotate (minus means rotate left, plus means rotate right)</param>
        public Rotated(IText text, int shift) : base(() =>
            {
                var str = text.AsString();
                int length = str.Length;
                if (length != 0 && shift != 0 && shift % length != 0)
                {
                    var builder = new StringBuilder(length);
                    int offset = -(shift % length);
                    if (offset < 0)
                    {
                        offset = str.Length + offset;
                    }
                    str = builder.Append(
                        str.Substring(offset)
                    ).Append(
                        str.Substring(0, offset)
                    ).ToString();
                }
                return str;
            },
            false
        )
        { }
    }
}
