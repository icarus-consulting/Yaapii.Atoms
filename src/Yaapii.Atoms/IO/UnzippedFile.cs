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
using System.IO;
using System.IO.Compression;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Content of a file in a zip archive
    /// is tolerant to slash style
    /// </summary>
    public sealed class UnzippedFile : IInput
    {
        private readonly IInput zip;
        private readonly string filePath;
        private readonly ScalarOf<Stream> stream;
        /// <summary>
        /// Content of a file in a zip archive
        /// is tolerant to slash style
        /// leaves zip stream open
        /// </summary>
        public UnzippedFile(IInput zip, string virtualPath) : this(zip, virtualPath, true)
        { }

        /// <summary>
        /// Content of a file in a zip archive
        /// is tolerant to slash style
        /// </summary>
        public UnzippedFile(IInput zip, string virtualPath, bool leaveOpen)
        {
            this.zip = zip;
            this.filePath = virtualPath;

            this.stream = new ScalarOf<Stream>(() =>
            {
                Stream content;
                using (var archive = new ZipArchive(
                                        new ValidatedZip(this.zip).Stream(),
                                        ZipArchiveMode.Read,
                                        leaveOpen))
                {
                    var zipEntry =
                        new FirstOf<ZipArchiveEntry>(
                            new Filtered<ZipArchiveEntry>(entry =>
                                Path.GetFullPath(entry.FullName) == Path.GetFullPath(this.filePath),
                                archive.Entries
                            ),
                            new ArgumentException($"Cannot extract file '{this.filePath}' because it doesn't exist in the zip archive.")
                        ).Value();
                    content = Content(zipEntry);
                }
                this.zip.Stream().Position = 0;
                return content;
            });
        }

        public Stream Stream()
        {
            return this.stream.Value();
        }

        private Stream Content(ZipArchiveEntry zipEntry)
        {
            MemoryStream content = new MemoryStream();
            using (Stream zipEntryStream = zipEntry.Open())
            {
                zipEntryStream.CopyTo(content);
                zipEntryStream.Close();
                content.Seek(0, SeekOrigin.Begin);
            }
            return content;
        }



    }
}
