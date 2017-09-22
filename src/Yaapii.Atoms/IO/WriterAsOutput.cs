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
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// A <see cref="StreamWriter"/> as <see cref="IOutput"/>.
    /// </summary>
    public sealed class WriterAsOutput : IOutput, IDisposable
    {
        /// <summary>
        /// the writer
        /// </summary>
        private readonly StreamWriter _writer;

        /// <summary>
        /// encoding of the writer
        /// </summary>
        private readonly IScalar<Decoder> _decoder;

        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IOutput"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        public WriterAsOutput(StreamWriter wtr) : this(wtr, Encoding.UTF8)
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IOutput"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        /// <param name="enc">encoding of the streamwriter</param>
        /// <param name="max"></param>
        public WriterAsOutput(StreamWriter wtr, Encoding enc) : this(wtr,
            () => enc.GetDecoder())
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IOutput"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        /// <param name="ddr">decoder for the writer</param>
        /// <param name="max">maximum buffer size</param>
        public WriterAsOutput(StreamWriter wtr, Func<Decoder> fnc) : this(wtr, new ScalarOf<Decoder>(fnc))
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> as <see cref="IOutput"/>.
        /// </summary>
        /// <param name="wtr">a streamwriter</param>
        /// <param name="ddr">decoder for the writer</param>
        /// <param name="max">maximum buffer size</param>
        public WriterAsOutput(StreamWriter wtr, IScalar<Decoder> ddr)
        {
            this._writer = wtr;
            this._decoder = ddr;
        }

        public Stream Stream()
        {
            return new WriterAsOutputStream(this._writer, this._decoder);
        }

        public void Dispose()
        {
            ((IDisposable)this._writer).Dispose();
        }

    }
}
