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

using System.Text.RegularExpressions;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// Checks if a text starts with a given content.
    /// </summary>
    public sealed class StartsWithText : IScalar<bool>
    {
        private readonly IText _text;
        private readonly IText _start;

        /// <summary>
        /// Checks if a <see cref="IText"/> starts with a given <see cref="string"/>
        /// </summary>
        /// <param name="text">Text to test</param>
        /// <param name="start">Starting content to use in the test</param>
        public StartsWithText(IText text, string start) : this(
            text,
            new TextOf(start)
        )
        { }

        /// <summary>
        /// Checks if a <see cref="IText"/> starts with a given <see cref="IText"/>
        /// </summary>
        /// <param name="text">Text to test</param>
        /// <param name="start">Starting content to use in the test</param>
        public StartsWithText(IText text, IText start)
        {
            this._text = text;
            this._start = start;
        }

        /// <summary>
        /// Gets the result
        /// </summary>
        /// <returns>The result</returns>
        public bool Value()
        {
            var regex = new Regex("^" + Regex.Escape(this._start.AsString()));
            return regex.IsMatch(this._text.AsString());
        }
    }
}
