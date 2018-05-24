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

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> trimmed (removed whitespaces) on the right side.
    /// </summary>
    public sealed class TrimmedRightText : IText
    {
        private readonly IText text;
        private readonly IText trimText;

        /// <summary>
        /// A <see cref="string"/> trimmed (removed whitespaces) on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        public TrimmedRightText(string text) : this(new TextOf(text))
        {
        }

        /// <summary>
        /// A <see cref="IText"/> trimmed (removed whitespaces) on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        public TrimmedRightText(IText text) : this(text, new TextOf("\b\f\n\r\t\v "))
        {
            //this.text = text;
        }
        /// <summary>
        /// A <see cref="string"/> trimmed with another <see cref="string"/> on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public TrimmedRightText(string text, string trimText) : this(new TextOf(text), new TextOf(trimText))
        {
        }
        /// <summary>
        /// A <see cref="string"/> trimmed with a <see cref="char"/>[] on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public TrimmedRightText(string text, char[] trimText) : this(new TextOf(text), new TextOf(trimText))
        {
        }

        /// <summary>
        /// A <see cref="string"/> trimmed with a <see cref="IText"/> on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public TrimmedRightText(string text, IText trimText) : this(new TextOf(text), trimText)
        {
        }

        /// <summary>
        /// A <see cref="IText"/> trimmed with a <see cref="string"/> on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public TrimmedRightText(IText text, string trimText) : this(text, new TextOf(trimText))
        {
        }
        /// <summary>
        /// A <see cref="IText"/> trimmed with a <see cref="char"/>[] on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public TrimmedRightText(IText text, char[] trimText) : this(text, new TextOf(trimText))
        {
        }

        /// <summary>
        /// A <see cref="IText"/> trimmed with another <see cref="IText"/> on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public TrimmedRightText(IText text, IText trimText)
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
            return this.text.AsString().TrimEnd(
                this.trimText.AsString().ToCharArray()
            );
        }

        /// <summary>
        /// Check for equality.
        /// </summary>
        /// <param name="other">other object to compare to</param>
        /// <returns>true if equal.</returns>
        public bool Equals(IText other)
        {
            return this.AsString().CompareTo(other.AsString()) == 0;
        }
    }
}
