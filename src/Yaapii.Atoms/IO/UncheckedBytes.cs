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
using System.IO;
using System.Text;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// <see cref="IBytes"/> that does not throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
    /// </summary>
    public sealed class UncheckedBytes : IBytes
    {
        /// <summary>
        /// the bytes
        /// </summary>
        private readonly IBytes _bytes;

        /// <summary>
        /// fallback function
        /// </summary>
        private readonly IFunc<IOException, byte[]> _fallback;

        /// <summary>
        /// <see cref="IBytes"/> that does not throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="bts">the bytes</param>
        public UncheckedBytes(IBytes bts) : this(
                bts, (error) => throw new UncheckedIOException(error))
        { }

        /// <summary>
        /// <see cref="IBytes"/> that does not throw <see cref="IOException"/> but calls given <see cref="Atoms.Func{IOException}"/> instead.
        /// </summary>
        /// <param name="bts"></param>
        /// <param name="fallback"></param>
        public UncheckedBytes(IBytes bts, System.Func<IOException, byte[]> fallback) : this(bts, new FuncOf<IOException, byte[]>(fallback))
        { }

        /// <summary>
        /// <see cref="IBytes"/> that does not throw <see cref="IOException"/> but calls given <see cref="Atoms.Func{IOException}"/> instead.
        /// </summary>
        /// <param name="bts"></param>
        /// <param name="fallback"></param>
        public UncheckedBytes(IBytes bts, IFunc<IOException, byte[]> fallback)
        {
            this._bytes = bts;
            this._fallback = fallback;
        }

        public byte[] AsBytes()
        {
            byte[] data;
            try
            {
                data = this._bytes.AsBytes();
            }
            catch (IOException ex)
            {
                data = new UncheckedFunc<IOException, byte[]>(this._fallback).Invoke(ex);
            }
            return data;
        }

    }
}
