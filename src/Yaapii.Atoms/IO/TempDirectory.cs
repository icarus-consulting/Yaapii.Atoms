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

using SymbolicLinkSupport;
using System;
using System.IO;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// A directory that cleans up when disposed.
    /// </summary>
    public sealed class TempDirectory : IScalar<DirectoryInfo>, IDisposable
    {
        private readonly IScalar<string> path;

        /// <summary>
        /// A directory that cleans up when disposed.
        /// </summary>
        public TempDirectory() : this(
            new ScalarOf<string>(() =>
                Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString())
            )
        )
        { }

        /// <summary>
        /// A directory that cleans up when disposed.
        /// </summary>
        public TempDirectory(string path) : this(
            new Live<string>(path)
        )
        { }

        /// <summary>
        /// A directory that cleans up when disposed.
        /// </summary>
        private TempDirectory(IScalar<string> path)
        {
            this.path = path;
        }

        public void Dispose()
        {
            if (Directory.Exists(path.Value()))
            {
                DeleteDirectory(path.Value());
            }
        }

        public DirectoryInfo Value()
        {
            if (!Directory.Exists(this.path.Value()))
            {
                Directory.CreateDirectory(this.path.Value());
            }
            return new DirectoryInfo(this.path.Value());
        }

        private void DeleteDirectory(string path)
        {
            var isNoSymLink = !new DirectoryInfo(path).IsSymbolicLink();
            if (isNoSymLink)
            {
                foreach (string subDir in Directory.GetDirectories(path))
                {
                    DeleteDirectory(subDir);
                }

                foreach (string fileName in Directory.EnumerateFiles(path))
                {
                    var fileInfo = new FileInfo(fileName)
                    {
                        Attributes = FileAttributes.Normal
                    };
                    fileInfo.Delete();
                }
            }
            Directory.Delete(path, recursive: isNoSymLink);
        }
    }
}
