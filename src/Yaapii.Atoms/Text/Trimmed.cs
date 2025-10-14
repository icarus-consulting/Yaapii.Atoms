// MIT License
//
// Copyright(c) 2025 ICARUS Consulting GmbH
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

using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// An <see cref="IText"/> without whitespaces / control characters or defined letters or a defined text on both sides.
    /// </summary>
    public sealed class Trimmed : TextEnvelope
    {
        /// <summary>
        /// A <see cref="string"/> trimmed (removed whitespaces) on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        public Trimmed(string text) : this(new LiveText(text))
        { }

        /// <summary>
        /// An <see cref="IText"/> trimmed (removed whitespaces) on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        public Trimmed(IText text) : this(
            text,
            new Live<char[]>(() => new char[] { '\b', '\f', '\n', '\r', '\t', '\v', ' ' })
        )
        { }

        /// <summary>
        /// A <see cref="string"/> trimmed with a <see cref="char"/>[] on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public Trimmed(string text, char[] trimText) : this(new LiveText(text), trimText)
        { }

        /// <summary>
        /// An <see cref="IText"/> trimmed with a <see cref="char"/>[] on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public Trimmed(IText text, char[] trimText) : this(text, new Live<char[]>(trimText))
        { }

        /// <summary>
        /// An <see cref="IText"/> trimmed with an IScalar&lt;<see cref="char"/>[]&gt; on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public Trimmed(IText text, IScalar<char[]> trimText) : base(
            () => text.AsString().Trim(trimText.Value()),
            false
        )
        { }

        /// <summary>
        /// A <see cref="string"/> from which a <see cref="string"/> is removed on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public Trimmed(string text, string removeText) : this(new LiveText(text), new LiveText(removeText))
        { }

        /// <summary>
        /// A <see cref="string"/> from which an <see cref="IText"/> is removed on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public Trimmed(string text, IText removeText) : this(new LiveText(text), removeText)
        { }

        /// <summary>
        /// An <see cref="IText"/> from which a <see cref="string"/> is removed on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public Trimmed(IText text, string removeText) : this(text, new LiveText(removeText))
        { }

        /// <summary>
        /// An <see cref="IText"/> from which an IScalar&lt;<see cref="IText"/>&gt; is removed on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public Trimmed(IText text, IText removeText) : this(
            text,
            removeText,
            false
        )
        { }

        /// <summary>
        /// An <see cref="IText"/> from which an IScalar&lt;<see cref="IText"/>&gt; is removed on both sides.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="ignoreCase">Trim by disregarding case.</param>
        /// <param name="removeText">text that is removed from the text</param>
        public Trimmed(IText text, IText removeText, bool ignoreCase) : base(
            () =>
            {
                string str = text.AsString();
                string remove = removeText.AsString();

                if (ignoreCase)
                {
                    var lower = str.ToLower();
                    var remLower = remove.ToLower();

                    while (str.StartsWith(remove) || str.EndsWith(remove))
                    {

                        if (lower.StartsWith(remLower))
                        {
                            str = str.Remove(0, remove.Length);
                        }
                        if (str.ToLower().EndsWith(remLower))
                        {
                            int startIndex = str.Length - remove.Length;
                            str = str.Remove(startIndex, remove.Length);
                        }
                    }
                }
                else
                {
                    while (str.StartsWith(remove) || str.EndsWith(remove))
                    {
                        if (str.StartsWith(remove))
                        {
                            str = str.Remove(0, remove.Length);
                        }
                        if (str.EndsWith(remove))
                        {
                            int startIndex = str.Length - remove.Length;
                            str = str.Remove(startIndex, remove.Length);
                        }
                    }
                }
                return str;
            },
            false
        )
        { }
    }
}
