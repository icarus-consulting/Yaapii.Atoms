/// MIT License
///
/// Copyright(c) 2017 ICARUS Consulting GmbH
///
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
///
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
///
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.

using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A text as a <see cref="Uri"/>
    /// </summary>
    public sealed class StringAsUri : IScalar<Uri>
    {
        private readonly IText _source;

        /// <summary>
        /// A <see cref="string"/> as a <see cref="Uri"/>
        /// </summary>
        /// <param name="uri">uri as a string</param>
        public StringAsUri(String url) : this(new TextOf(url))
        { }

        /// <summary>
        /// A <see cref="IText"/> as a <see cref="Uri"/>
        /// </summary>
        /// <param name="url">uri as text</param>
        public StringAsUri(IText url)
        {
            this._source = url;
        }

        public Uri Value()
        {
            return new UriBuilder(this._source.AsString()).Uri;
        }
    }
}
