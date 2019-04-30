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
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// A zip input mapped from a given zip input
    /// Maps the zip entry paths according to the given mapping function
    /// </summary>
    public sealed class ZipMappedPaths : IInput
    {
        private readonly IScalar<Stream> input;

        /// <summary>
        /// A zip input mapped from a given zip input
        /// Maps the zip entry paths according to the given mapping function
        /// </summary>
        public ZipMappedPaths(Func<string, string> mapping, IInput zip)
        {
            this.input =
                new Sticky<Stream>(() =>
                {
                    Stream inMemory = new ValidatedZip(zip).Stream();
                    var newMemory = new MemoryStream();

                    using (var archive = new ZipArchive(inMemory, ZipArchiveMode.Read, true))
                    using (var newArchive = new ZipArchive(newMemory, ZipArchiveMode.Create, true))
                    {
                        foreach (var entry in archive.Entries)
                        {
                            Move(entry, newArchive, mapping);
                        }
                        inMemory.Position = 0;
                    }
                    newMemory.Seek(0, SeekOrigin.Begin);
                    return newMemory;
                });
        }
        public Stream Stream()
        {
            return this.input.Value();
        }

        private void Move(ZipArchiveEntry source, ZipArchive archive, Func<string, string> mapping)
        {
            var mapped = mapping(source.FullName);
            using (var sourceStream = source.Open())
            using (var stream = archive.CreateEntry(mapped).Open())
            {
                new Atoms.IO.LengthOf(
                    new TeeInput(
                        new InputOf(sourceStream),
                        new OutputTo(stream)
                    )
                ).Value();
            }
        }
    }
}
