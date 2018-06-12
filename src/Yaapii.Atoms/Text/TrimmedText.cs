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
    /// An <see cref="IText"/> without whitespaces / control characters or defined letters or a defined text on both sides.
    /// </summary>
    public sealed class TrimmedText : IText
    {
        private readonly IScalar<IText> trimmedText;

        /// <summary>
        /// A <see cref="string"/> trimmed (removed whitespaces) on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        public TrimmedText(string text) : this(new TextOf(text))
        { }

        /// <summary>
        /// An <see cref="IText"/> trimmed (removed whitespaces) on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        public TrimmedText(IText text) : this(text, new ScalarOf<char[]>(() => new char[] { '\b', '\f', '\n', '\r', '\t', '\v', ' ' }))
        { }

        /// <summary>
        /// A <see cref="string"/> trimmed with a <see cref="char"/>[] on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public TrimmedText(string text, char[] trimText) : this(new TextOf(text), trimText)
        { }

        /// <summary>
        /// An <see cref="IText"/> trimmed with a <see cref="char"/>[] on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public TrimmedText(IText text, char[] trimText) : this(text, new ScalarOf<char[]>(trimText))
        { }

        /// <summary>
        /// An <see cref="IText"/> trimmed with an IScalar&lt;<see cref="char"/>[]&gt; on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public TrimmedText(IText text, IScalar<char[]> trimText) : this(
            new ScalarOf<IText>(
                () =>
                {
                    return new TextOf(text.AsString().Trim(trimText.Value()));
                }
            )
        )
        { }

        /// <summary>
        /// A <see cref="string"/> from which a <see cref="string"/> is removed on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public TrimmedText(string text, string removeText) : this(new TextOf(text), new TextOf(removeText))
        { }

        /// <summary>
        /// A <see cref="string"/> from which an <see cref="IText"/> is removed on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public TrimmedText(string text, IText removeText) : this(new TextOf(text), removeText)
        { }

        /// <summary>
        /// An <see cref="IText"/> from which a <see cref="string"/> is removed on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public TrimmedText(IText text, string removeText) : this(text, new TextOf(removeText))
        { }

        /// <summary>
        /// An <see cref="IText"/> from which an <see cref="IText"/> is removed on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public TrimmedText(IText text, IText removeText) : this(text, new ScalarOf<IText>(removeText))
        { }

        /// <summary>
        /// An <see cref="IText"/> from which an IScalar&lt;<see cref="IText"/>&gt; is removed on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public TrimmedText(IText text, IScalar<IText> removeText) : this(
            new ScalarOf<IText>(
                () =>
                {
                    IText returnValue = text;
                    var startsWith =
                        text.AsString()
                        .StartsWith(
                            removeText.Value().AsString()
                        );
                    if (startsWith)
                    {
                        text = new TextOf(text.AsString().Remove(0, removeText.Value().AsString().Length));
                        returnValue = text;
                    }
                    var endsWith =
                        text.AsString()
                        .EndsWith(
                            removeText.Value().AsString()
                        );
                    if (endsWith)
                    {
                        int startIndex = text.AsString().Length - removeText.Value().AsString().Length;
                        text = new TextOf(text.AsString().Remove(startIndex, removeText.Value().AsString().Length));
                        returnValue = text;
                    }
                    return returnValue;
                }
            )
        )
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private TrimmedText(IScalar<IText> text)
        {
            this.trimmedText = text;
        }

        /// <summary>
        /// Get content as a string.
        /// </summary>
        /// <returns>the content as a string</returns>
        public String AsString()
        {
            return this.trimmedText.Value().AsString();
        }

        /// <summary>
        /// Compare to other text.
        /// </summary>
        /// <param name="text">text to compare to</param>
        /// <returns>-1 if this is lower, 0 if equal, 1 if this is higher</returns>
        public int CompareTo(IText text)
        {
            return this.AsString().CompareTo(text.AsString());
        }

        /// <summary>
        /// Check for equality.
        /// </summary>
        /// <param name="other">other object to compare to</param>
        /// <returns>true if equal.</returns>
        public bool Equals(IText other)
        {
            return this.AsString().Equals(other.AsString());
        }
    }
}
