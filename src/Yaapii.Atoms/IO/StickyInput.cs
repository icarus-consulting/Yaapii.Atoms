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
using System.IO;
using System.Text;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// <see cref="IInput"/> that reads once and then returns from cache.
    /// </summary>
    public sealed class StickyInput : IInput
    {
        /// <summary>
        /// the cache
        /// </summary>
        private readonly IScalar<byte[]> _cache;

        /// <summary>
        /// <see cref="IInput"/> that reads once and then returns from cache.
        /// Closes the input stream after first read.
        /// </summary>
        /// <param name="input"></param>
        public StickyInput(IInput input)
        {
            this._cache = new StickyScalar<byte[]>(
                new ScalarOf<byte[]>(() =>
                    {
                        MemoryStream baos = new MemoryStream();
                        new LengthOf(
                            new TeeInput(input, new OutputTo(baos))
                        ).Value();
                        input.Stream().Dispose();
                        return baos.ToArray();
                    }));
        }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            return new MemoryStream(this._cache.Value());
        }
    }
}
