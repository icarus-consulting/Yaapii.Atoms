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

using System;
using System.Net;
using System.Text;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Decoded url from a string.
    /// </summary>
    public sealed class DecodedUrl : IScalar<String>
    {
        /// <summary>
        /// source text
        /// </summary>
        private readonly IText source;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="url">url as string</param>
        public DecodedUrl(String url) : this(url, Encoding.UTF8)
        { }

        /// <summary>
        /// Decoded url from a string.
        /// </summary>
        /// <param name="url">url as string</param>
        /// <param name="enc">encoding of the string</param>
        public DecodedUrl(String url, Encoding enc) : this(new TextOf(url, enc))
        { }

        /// <summary>
        /// Decoded url from a string.
        /// </summary>
        /// <param name="url">url as text</param>
        public DecodedUrl(IText url)
        {
            this.source = url;
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>the value</returns>
        public String Value()
        {
            return WebUtility.UrlDecode(
                this.source.AsString()
            );
        }
    }
}
