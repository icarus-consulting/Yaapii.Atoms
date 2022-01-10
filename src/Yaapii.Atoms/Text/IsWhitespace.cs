// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
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
using System.Linq;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// Checks if a text is whitespace.
    /// </summary>
    public sealed class IsWhitespace : IScalar<Boolean>
    {
        private readonly ScalarOf<bool> result;

        /// <summary>
        /// Checks if a A <see cref="string"/> is whitespace.
        /// </summary>
        /// <param name="text">text to check</param>
        public IsWhitespace(string text) : this(
            new TextOf(text)
        )
        { }

        /// <summary>
        /// Checks if a A <see cref="IText"/> is whitespace.
        /// </summary>
        /// <param name="text">text to check</param>
        public IsWhitespace(IText text)
        {
            this.result = new ScalarOf<bool>(() => !text.AsString().ToCharArray().Any(c => !String.IsNullOrWhiteSpace(c + "")));
        }

        /// <summary>
        /// Get the result.
        /// </summary>
        /// <returns>the result</returns>
        public Boolean Value()
        {
            return this.result.Value();
        }
    }
}
