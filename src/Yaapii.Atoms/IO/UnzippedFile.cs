using System;
using System.IO;
using System.IO.Compression;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
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

        /// <summary>
        /// Content of a file in a zip archive
        /// is tolerant to slash style
        /// </summary>
        public UnzippedFile(IInput zip, string virtualPath)
        {
            this.zip = zip;
            this.filePath = virtualPath;
        }

        public Stream Stream()
        {
            Stream content;
            using (var archive = new ZipArchive(this.zip.Stream(), ZipArchiveMode.Read, true))
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
            return content;
        }

        private Stream Content(ZipArchiveEntry zipEntry)
        {
            MemoryStream content = new MemoryStream();
            Stream zipEntryStream = zipEntry.Open();
            zipEntryStream.CopyTo(content);
            zipEntryStream.Close();
            content.Seek(0, SeekOrigin.Begin);
            return content;
        }
    }
}
