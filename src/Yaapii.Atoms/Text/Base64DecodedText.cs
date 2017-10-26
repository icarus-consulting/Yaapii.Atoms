﻿// MIT License
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
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> as Base64 decoded <see cref="IText"/>
    /// </summary>
    public sealed class Base64DecodedText : IText
    {
        private readonly IText _origin;

        /// <summary>
        /// A <see cref="string"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="str">string to decode</param>
        public Base64DecodedText(String str) : this(new TextOf(str))
        { }

        /// <summary>
        /// A <see cref="IText"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="text">text to decode</param>
        public Base64DecodedText(IText text)
        {
            this._origin = text;
        }

        /// <summary>
        /// Get content as a string.
        /// </summary>
        /// <returns>the content as a string</returns>
        public String AsString()
        {
            return new TextOf(
                Convert.FromBase64String(this._origin.AsString())).AsString();
        }

        /// <summary>
        /// Check for equality.
        /// </summary>
        /// <param name="text">other object to compare to</param>
        /// <returns>true if equal.</returns>
        public bool Equals(IText text)
        {
            return this.Equals(text);
        }
    }
}
