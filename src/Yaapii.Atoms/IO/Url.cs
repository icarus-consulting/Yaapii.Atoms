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

using System;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// www <see cref="Uri"/> from a <see cref="string"/> which checks for format.
    /// </summary>
    public sealed class Url : IScalar<Uri>
    {
        /// <summary>
        /// the source
        /// </summary>
        private readonly string _source;

        /// <summary>
        /// Url from a <see cref="string"/> which checks for format.
        /// </summary>
        /// <param name="src">url prefixed with http:// or https://</param>
        public Url(string src)
        {
            _source = src;
        }

        /// <summary>
        /// Get the uri.
        /// </summary>
        /// <returns></returns>
        public Uri Value()
        {
            if (!_source.StartsWith("http://") && !_source.StartsWith("https://") && !_source.StartsWith("ftp://"))
            {
                throw new ArgumentException("url must start with http or https");
            }
            return new Uri(_source);
        }
    }
}
