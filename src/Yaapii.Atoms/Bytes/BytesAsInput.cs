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
using System.IO;
using Yaapii.Atoms.IO;

namespace Yaapii.Atoms.Bytes
{
    /// <summary>
    /// Bytes as input.
    /// </summary>
    public sealed class BytesAsInput : IInput
    {
        /// <summary>
        /// the source
        /// </summary>
        private readonly IBytes source;

        /// <summary>
        /// Bytes as input.
        /// </summary>
        /// <param name="text">a text</param>
        public BytesAsInput(IText text) : this(new BytesOf(text))
        { }

        /// <summary>
        /// Bytes as input.
        /// </summary>
        /// <param name="text">a string</param>
        public BytesAsInput(String text) : this(new BytesOf(text))
        { }

        /// <summary>
        /// Bytes as input.
        /// </summary>
        /// <param name="bytes">byte array</param>
        public BytesAsInput(byte[] bytes) : this(new BytesOf(bytes))
        { }

        /// <summary>
        /// Bytes as input.
        /// </summary>
        /// <param name="bytes">bytes</param>
        public BytesAsInput(IBytes bytes)
        {
            this.source = bytes;
        }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            return new MemoryStream(this.source.AsBytes());
        }
    }
}
