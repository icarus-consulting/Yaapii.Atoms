// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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
using System.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// A <see cref="IOutput"/> which will copy to another <see cref="IOutput"/> while writing.
    /// </summary>
    public sealed class TeeOutput : IOutput, IDisposable
    {
        private readonly IOutput _target;
        private readonly IOutput _copy;

        /// <summary>
        /// A <see cref="IOutput"/> which will copy to another <see cref="IOutput"/> while writing.
        /// </summary>
        /// <param name="tgt">the original target</param>
        /// <param name="cpy">the copy target</param>
        public TeeOutput(IOutput tgt, StreamWriter cpy) : this(tgt, new OutputTo(cpy))
        { }

        /// <summary>
        /// A <see cref="IOutput"/> which will copy to another <see cref="StreamWriter"/> while writing.
        /// </summary>
        /// <param name="tgt">the original target</param>
        /// <param name="cpy">the copy target</param>
        /// <param name="enc">encoding for the copy target</param>
        public TeeOutput(IOutput tgt, StreamWriter cpy, Encoding enc) : this(tgt, new OutputTo(cpy, enc))
        { }

        /// <summary>
        /// A <see cref="IOutput"/> which will copy to a file <see cref="Uri"/> while writing.
        /// </summary>
        /// <param name="tgt">the original target</param>
        /// <param name="cpy">the copy target file <see cref="Uri"/></param>
        public TeeOutput(IOutput tgt, Uri cpy) : this(tgt, new OutputTo(cpy))
        { }

        /// <summary>
        /// A <see cref="IOutput"/> which will copy to a <see cref="Stream"/> while writing.
        /// </summary>
        /// <param name="tgt">the original target</param>
        /// <param name="cpy">the copy target file <see cref="Uri"/></param>        
        public TeeOutput(IOutput tgt, Stream cpy) : this(tgt, new OutputTo(cpy))
        { }

        /// <summary>
        /// A <see cref="IOutput"/> which will copy to another <see cref="IOutput"/> while writing.
        /// </summary>
        /// <param name="tgt">the original target</param>
        /// <param name="cpy">the copy target file <see cref="Uri"/></param>        
        public TeeOutput(IOutput tgt, IOutput cpy)
        {
            this._target = tgt;
            this._copy = cpy;
        }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            return new TeeOutputStream(
                this._target.Stream(), this._copy.Stream()
            );
        }

        /// <summary>
        /// Clean up
        /// </summary>
        public void Dispose()
        {
            (_target as IDisposable)?.Dispose();
            (_copy as IDisposable)?.Dispose();
        }

    }
}
