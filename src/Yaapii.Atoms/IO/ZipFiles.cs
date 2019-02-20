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

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// The files in a ZIP archive.
    /// Note: Extraction is sticky.
    /// </summary>
    public sealed class ZipFiles : IEnumerable<string>
    {
        private readonly IScalar<IEnumerable<string>> files;

        /// <summary>
        /// The files in a ZIP archive.
        /// Note: Extraction is sticky.
        /// leaves zip stream open
        /// </summary>
        /// <param name="input"></param>
        public ZipFiles(IInput input) : this(input, true)
        {
           
        }

        public ZipFiles(IInput input, bool leaveOpen)
        {
            this.files =
                new StickyScalar<IEnumerable<string>>(() =>
                {
                    try
                    {
                        IEnumerable<string> files;
                        var copy = new MemoryStream();
                        var inputStream = input.Stream();
                        inputStream.Position = 0;
                        inputStream.CopyTo(copy);
                        inputStream.Position = 0;
                        copy.Position = 0;

                        using (var zip = new ZipArchive(copy, ZipArchiveMode.Read, leaveOpen))
                        {
                            files =
                                new Mapped<ZipArchiveEntry, string>(
                                    entry => entry.FullName,
                                    zip.Entries
                                );
                        }
                        return files;
                    }
                    finally
                    {
                        if (!leaveOpen)
                        {
                            input.Stream().Close();
                        }
                    }
                });
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this.files.Value().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
