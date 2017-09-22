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
    /// <see cref="IOutput"/> that doesn't throw <see cref="IOException"/> but throws <see cref="Atoms.Error.UncheckedIOException"/> instead.
    /// </summary>
    public sealed class UncheckedOutput : IOutput, IDisposable
    {
        /// <summary>
        /// the output
        /// </summary>
        private readonly IOutput _output;

        /// <summary>
        /// <see cref="IOutput"/> that doesn't throw <see cref="IOException"/> but throws <see cref="Atoms.Error.UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="opt">output to uncheck</param>
        public UncheckedOutput(IOutput opt)
        {
            this._output = opt;
        }

        public Stream Stream()
        {
            return new UncheckedScalar<Stream>(
                    new ScalarOf<Stream>(
                        this._output.Stream())
                   ).Value();
        }

        public void Dispose()
        {
            ((IDisposable)this._output.Stream()).Dispose();
        }
    }
}
