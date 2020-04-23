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

using System.Text.RegularExpressions;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Texts
{
    /// <summary>
    /// Checks if a text ends with a given content.
    /// </summary>
    public sealed class EndsWith : IScalar<bool>
    {
        private readonly ScalarOf<bool> result;

        /// <summary>
        /// Checks if a <see cref="IText"/> ends with a given <see cref="string"/>
        /// </summary>
        /// <param name="text">Text to test</param>
        /// <param name="tail">Ending content to use in the test</param>
        public EndsWith(IText text, string tail) : this(
            text,
            new LiveText(tail)
        )
        { }

        /// <summary>
        /// Checks if a <see cref="IText"/> ends with a given <see cref="IText"/>
        /// </summary>
        /// <param name="text">Text to test</param>
        /// <param name="tail">Ending content to use in the test</param>
        public EndsWith(IText text, IText tail)
        {
            this.result =
                new ScalarOf<bool>(() =>
                {
                    var regex = new Regex(Regex.Escape(tail.AsString()) + "$");
                    return regex.IsMatch(text.AsString());
                });
        }

        /// <summary>
        /// Gets the result
        /// </summary>
        /// <returns>The result</returns>
        public bool Value()
        {
            return this.result.Value();
        }
    }
}
