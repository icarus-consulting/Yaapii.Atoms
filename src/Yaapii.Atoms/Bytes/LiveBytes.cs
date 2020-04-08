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
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Bytes
{
    /// <summary>
    /// Bytes out of other objects that are reloaded on every call
    /// </summary>
    public sealed class LiveBytes : IBytes
    {
        private readonly IScalar<IBytes> bytes;

        /// <summary>
        /// Reloads the bytes input on every call
        /// </summary>
        /// <param name="input">The input</param>
        public LiveBytes(Func<IInput> input) : this(() => new BytesOf(input()))
        { }

        /// <summary>
        /// Relaods the bytes on every call
        /// </summary>
        /// <param name="bytes"></param>
        public LiveBytes(Func<IBytes> bytes) : this(new LiveScalar<IBytes>(bytes))
        { }

        private LiveBytes(IScalar<IBytes> bytes)
        {
            this.bytes = bytes;
        }

        public byte[] AsBytes()
        {
            return this.bytes.Value().AsBytes();
        }
    }
}
