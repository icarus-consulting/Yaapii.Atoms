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
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.IO;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> as Base64 encoded <see cref="IText"/>
    /// </summary>
    public sealed class TextBase64 : IText
    {
        private readonly IText origin;

        /// <summary>
        /// A <see cref="string"/> as Base64-Encoded <see cref="IText"/>
        /// </summary>
        /// <param name="str">string to encode</param>
        public TextBase64(String str) : this(new TextOf(str))
        { }

        /// <summary>
        /// A <see cref="IText"/> as Base64-Encoded <see cref="IText"/>
        /// </summary>
        /// <param name="text">text to encode</param>
        public TextBase64(IText text)
        {
            this.origin =
                new TextOf(
                    new BytesBase64(
                        new BytesOf(text)
                    )
                );
        }

        public string AsString()
        {
            return origin.AsString();
                
        }

        public bool Equals(IText other)
        {
            return origin.Equals(other);
        }
    }
}
