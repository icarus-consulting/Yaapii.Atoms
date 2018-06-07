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
    /// A <see cref="IText"/> from which another text is removed on the left side.
    /// </summary>
    public sealed class RemovedLeftText : IText
    {
        private readonly IText text;
        private readonly IText removeText;

        /// <summary>
        /// A <see cref="string"/> from which a <see cref="string"/> is removed on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that trims the text</param>
        public RemovedLeftText(string text, string removeText) : this(new TextOf(text), new TextOf(removeText))
        {
        }

        /// <summary>
        /// A <see cref="string"/> from which a <see cref="IText"/> is removed on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that trims the text</param>
        public RemovedLeftText(string text, IText removeText) : this(new TextOf(text), removeText)
        {
        }

        /// <summary>
        /// A <see cref="IText"/> from which a <see cref="string"/> is removed on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that trims the text</param>
        public RemovedLeftText(IText text, string removeText) : this(text, new TextOf(removeText))
        {
        }

        /// <summary>
        /// A <see cref="IText"/> from which a <see cref="IText"/> is removed on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that trims the text</param>
        public RemovedLeftText(IText text, IText removeText)
        {
            this.text = text;
            this.removeText = removeText;
        }

        /// <summary>
        /// Get content as a string.
        /// </summary>
        /// <returns>the content as a string</returns>
        public String AsString()
        {
            var startsWith =
                this.text.AsString()
                .StartsWith(
                    this.removeText.AsString()
                );
            if (startsWith)
            {
                return this.text.AsString().Remove(0, this.removeText.AsString().Length);
            }
            return this.text.AsString();
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
