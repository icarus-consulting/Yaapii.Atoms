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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> as uppercase.
    /// </summary>
    public sealed class Upper : IText
    {
        private readonly IText _origin;

        /// <summary>
        /// A <see cref="IText"/> as uppercase.
        /// </summary>
        /// <param name="text">text to uppercase</param>
        public Upper(IText text)
        {
            this._origin = text;
        }

        /// <summary>
        /// Get content as a string.
        /// </summary>
        /// <returns>the content as a string</returns>
        public String AsString()
        {
            return this._origin.AsString().ToUpperInvariant();
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
        public bool Equals(Atoms.IText other)
        {
            return other.AsString().Equals(this.AsString());
        }
    }
}
