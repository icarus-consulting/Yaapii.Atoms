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
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> without whitespaces or control characters on both sides.
    /// </summary>
    public sealed class TrimmedText : IText
    {
        private readonly IText text;
        private readonly IScalar<char[]> trimText;

        /// <summary>
        /// A <see cref="string"/> trimmed (removed whitespaces) on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        public TrimmedText(string text) : this(new TextOf(text))
        {
        }

        /// <summary>
        /// A <see cref="IText"/> trimmed (removed whitespaces) on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        public TrimmedText(IText text) : this(text, new ScalarOf<char[]>(() => new char[] { '\b', '\f', '\n', '\r', '\t', '\v', ' ' }))
        {
        }

        /// <summary>
        /// A <see cref="string"/> trimmed with a <see cref="char"/>[] on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public TrimmedText(string text, char[] trimText) : this(new TextOf(text), trimText)
        {
        }

        /// <summary>
        /// A <see cref="IText"/> trimmed with a <see cref="char"/>[] on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public TrimmedText(IText text, char[] trimText) : this(text, new ScalarOf<char[]>(trimText))
        {
        }
        /// <summary>
        /// A <see cref="IText"/> trimmed with a IScalar<see cref="char"/>[] on the left side.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="trimText"></param>
        public TrimmedText(IText text, IScalar<char[]> trimText)
        {
            this.text = text;
            this.trimText = trimText;
        }

        /// <summary>
        /// Get content as a string.
        /// </summary>
        /// <returns>the content as a string</returns>
        public String AsString()
        {
            return this.text.AsString()
                .Trim(trimText.Value());
        }

        /// <summary>
        /// Compare to other text.
        /// </summary>
        /// <param name="text">text to compare to</param>
        /// <returns>-1 if this is lower, 0 if equal, 1 if this is higher</returns>
        public int CompareTo(IText text)
        {
            return this.CompareTo(text);
        }

        /// <summary>
        /// Check for equality.
        /// </summary>
        /// <param name="other">other object to compare to</param>
        /// <returns>true if equal.</returns>
        public bool Equals(Atoms.IText other)
        {
            return other.AsString().Equals(this.AsString());
        }
    }
}
