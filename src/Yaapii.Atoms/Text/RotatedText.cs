/// MIT License
///
/// Copyright(c) 2017 ICARUS Consulting GmbH
///
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
///
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
///
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.

using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> whose characters have been rotated.
    /// </summary>
    public sealed class RotatedText : IText
    {
        private readonly IText _origin;
        private readonly int _move;

        /// <summary>
        /// A <see cref="IText"/> whose characters have been rotated.
        /// </summary>
        /// <param name="text">text to rotate</param>
        /// <param name="shift">direction and amount of chars to rotate (minus means rotate left, plus means rotate right)</param>
        public RotatedText(IText text, int shift)
        {
            this._origin = text;
            this._move = shift;
        }

        public String AsString()
        {
            var text = this._origin.AsString();
            int length = text.Length;
            if (length != 0 && this._move != 0 && this._move % length != 0)
            {
                var builder = new StringBuilder(length);
                int offset = -(this._move % length);
                if (offset < 0)
                {
                    offset = text.Length + offset;
                }
                text = builder.Append(
                    text.Substring(offset)
                ).Append(
                    text.Substring(0, offset)
                ).ToString();
            }
            return text;
        }

        public bool Equals(IText text)
        {
            return new UncheckedText(this).CompareTo(text) == 0;
        }

    }
}
