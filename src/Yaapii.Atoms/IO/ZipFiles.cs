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
        /// </summary>
        /// <param name="input"></param>
        public ZipFiles(IInput input)
        {
            this.files =
                new StickyScalar<IEnumerable<string>>(() =>
                {
                    IEnumerable<string> files;
                    var copy = new MemoryStream();
                    input.Stream().Position = 0;
                    input.Stream().CopyTo(copy);
                    input.Stream().Position = 0;
                    copy.Position = 0;

                    using (var zip = new ZipArchive(copy, ZipArchiveMode.Read, true))
                    {
                        files =
                            new Mapped<ZipArchiveEntry, string>(
                                entry => entry.FullName,
                                zip.Entries
                            );
                    }
                    return files;
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
