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
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Temporary file.
    /// The temporary file is deleted when the object is disposed.
    /// </summary>
    public sealed class TempFile : IScalar<String>, IDisposable
    {
        private readonly IScalar<string> _path;

        /// <summary>
        /// Temporary file with given extension.
        /// The temporary file is deleted when the object is disposed.
        /// </summary>
        /// <param name="extension">The file extension for the temprary file</param>
        public TempFile(string extension) : this(new ScalarOf<string>(() =>
        {
            var file = Path.GetTempFileName();
            extension = extension.TrimStart('.');
            var renamed = $"{file.Substring(0, file.LastIndexOf('.'))}.{extension}";
            File.Move(file, renamed);
            return renamed;
        }))
        { }

        /// <summary>
        /// The temporary file is deleted when the object is disposed.
        /// </summary>
        /// <param name="file">The file</param>
        public TempFile(FileInfo file) : this(new ScalarOf<string>(() => file.FullName))
        { }

        /// <summary>
        /// Ctor
        /// </summary>
        public TempFile() :
            this(new ScalarOf<string>(() => Path.GetTempFileName()))
        { }

        private TempFile(IScalar<string> path)
        {
            _path = path;
        }

        /// <summary>
        /// Temporary file's path.
        /// The first call create the temporary file.
        /// </summary>
        public string Value()
        {
            return _path.Value();
        }

        /// <summary>
        /// Delete the temporary file.
        /// </summary>
        public void Dispose()
        {
            File.Delete(_path.Value());
        }
    }
}
