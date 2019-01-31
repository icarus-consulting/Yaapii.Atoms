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
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.OutputDeck
{
    /// <summary>
    /// <see cref="IOutput"/> which does not accept null as stream
    /// </summary>
    public sealed class OutputNoNulls : IOutput, IDisposable
    {
        /// <summary>
        /// the origin
        /// </summary>
        private readonly IOutput _origin;

        /// <summary>
        /// <see cref="IOutput"/> which does not accept null as stream.
        /// </summary>
        /// <param name="output">the output</param>
        public OutputNoNulls(IOutput output)
        {
            this._origin = output;
        }

        /// <summary>
        /// The stream.
        /// </summary>
        /// <returns>the stream, throws IOException if stream is null</returns>
        public Stream Stream()
        {
            if (this._origin == null)
            {
                throw new IOException("got NULL instead of a valid output");
            }

            var stream = this._origin.Stream();
            if (stream == null)
            {
                throw new IOException("got NULL instead of a valid stream");
            }
            return stream;
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        public void Dispose()
        {
            (_origin as IDisposable)?.Dispose();
        }
    }
}
