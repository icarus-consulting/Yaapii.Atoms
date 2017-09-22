﻿/// MIT License
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
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// <see cref="IOutput"/> to a target.
    /// </summary>
    public sealed class OutputTo : IOutput, IDisposable
    {
        /// <summary>
        /// the output
        /// </summary>
        private readonly IScalar<Stream> _origin;

        /// <summary>
        /// a path
        /// </summary>
        /// <param name="path"></param>
        public OutputTo(string path) : this(new Uri(path))
        { }

        /// <summary>
        /// <see cref="IOutput"/> to a target file Uri.
        /// </summary>
        /// <param name="path">a file uri, retrieve with Path.GetFullPath(absOrRelativePath) or prefix with file://. Must be absolute</param>
        public OutputTo(Uri path) : this(
            () => new FileStream(path.AbsolutePath, FileMode.OpenOrCreate))
        { }

        /// <summary>
        /// <see cref="IOutput"/> to a target <see cref="StreamWriter"/>.
        /// </summary>
        /// <param name="wtr">a writer</param>
        public OutputTo(StreamWriter wtr) : this(wtr, Encoding.UTF8)
        { }

        /// <summary>
        /// <see cref="IOutput"/> to a target <see cref="StreamWriter"/>.
        /// </summary>
        /// <param name="wtr">a writer</param>
        /// <param name="enc"><see cref="Encoding"/> of the writer</param>
        /// <param name="size">maximum buffer size</param>
        public OutputTo(StreamWriter wtr, Encoding enc) : this(
            new WriterAsOutputStream(wtr, enc))
        { }

        /// <summary>
        /// <see cref="IOutput"/> to a target <see cref="Stream"/> returned by a <see cref="Func{TResult}"/>.
        /// </summary>
        /// <param name="stream">target stream</param>
        public OutputTo(Func<Stream> fnc) : this(new ScalarOf<Stream>(fnc))
        { }

        /// <summary>
        /// <see cref="IOutput"/> to a target <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">target stream</param>
        public OutputTo(Stream stream) : this(new ScalarOf<Stream>(() => stream))
        { }

        private OutputTo(IScalar<Stream> origin)
        {
            this._origin = origin;
        }

        public Stream Stream()
        {
            return this._origin.Value();
        }
        public void Dispose()
        {
            ((IDisposable)this._origin.Value()).Dispose();
        }

    }
}
