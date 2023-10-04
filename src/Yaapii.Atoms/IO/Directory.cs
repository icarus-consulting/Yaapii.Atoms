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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO
{
    public sealed class DirectoryOf : System.Collections.Generic.IEnumerable<string>
    {

        /// <summary>
        /// Path of the directory.
        /// </summary>
        private readonly IScalar<string> _dir;

        /// <summary>
        /// include all files from sub directories
        /// </summary>
        private readonly IScalar<bool> _recursive;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="dir">DirectoryInfo</param>
        public DirectoryOf(DirectoryInfo dir) : this(dir, false)
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="dir">DirectoryInfo</param>
        /// <param name="recursive">include all files from sub directories</param>
        public DirectoryOf(DirectoryInfo dir, bool recursive) : this(new Live<string>(() =>
            {
                return dir.FullName;
            }),
            new Live<bool>(recursive)
        )
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="file">File as a uri</param>
        public DirectoryOf(Uri file) : this(file, false)
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="file">File as a uri</param>
        /// <param name="recursive">include all files from sub directories</param>
        public DirectoryOf(Uri file, bool recursive) : this(new Live<string>(() =>
            {
                if (file.Scheme != "file")
                {
                    throw
                     new ArgumentException(
                         new Formatted("'{0}' is not a directory.", file.ToString()).AsString()
                     );
                }
                return file.AbsolutePath;
            }),
            new Live<bool>(recursive)
        )
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="file">File as a path to directory.</param>
        public DirectoryOf(FileInfo file) : this(file, false)
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="file">File as a path to directory.</param>
        /// <param name="recursive">include all files from sub directories</param>
        public DirectoryOf(FileInfo file, bool recursive) : this(new Live<string>(file.Directory.FullName), new Live<bool>(recursive))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path"></param>
        public DirectoryOf(string path) : this(path, false)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recursive">include all files from sub directories</param>
        public DirectoryOf(string path, bool recursive) : this(new Live<string>(path), new Live<bool>(recursive))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path"></param>
        public DirectoryOf(IScalar<string> path) : this(path, new False())
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recursive">include all files from sub directories</param>
        public DirectoryOf(IScalar<string> path, IScalar<bool> recursive)
        {
            this._dir = new ScalarOf<string>(() =>
            {
                var val = Path.GetFullPath(path.Value());
                try
                {
                    //check if path is a directory
                    if ((File.GetAttributes(val) & FileAttributes.Directory) != FileAttributes.Directory)
                    {
                        throw new ArgumentException();
                    }
                }
                catch (ArgumentException)
                {
                    throw
                        new ArgumentException(
                            new Formatted("'{0}' is not a directory.", path.Value()
                        ).AsString()
                    );
                }

                return val;
            });
            this._recursive = recursive;
        }

        /// <summary>
        /// Enumerate directory and file paths as string.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<string> GetEnumerator()
        {
            return new Ternary<bool, IEnumerator<string>>(
                this._recursive,
                new Sorted<string>(
                    Directory.EnumerateFiles(_dir.Value(), "*", SearchOption.AllDirectories)

                ).GetEnumerator(),
                new Sorted<string>(
                    new Joined<string>(
                        Directory.EnumerateDirectories(_dir.Value()),
                        Directory.EnumerateFiles(_dir.Value())
                    )
                ).GetEnumerator()
            ).Value();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
