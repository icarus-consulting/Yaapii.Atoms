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
        private readonly StickyScalar<Stream> stream;
        /// <summary>
        /// Content of a file in a zip archive
        /// is tolerant to slash style
        /// </summary>
        public UnzippedFile(IInput zip, string virtualPath)
        {
            this.zip = zip;
            this.filePath = virtualPath;

            this.stream = new StickyScalar<Stream>(() =>
            {
                new FailPrecise(
                    new FailWhen(!IsZip(zip.Stream())),
                    new InvalidOperationException($"Content is not compressed with either GZIP or PKZIP")
                ).Go();

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
            Stream zipEntryStream = zipEntry.Open();
            zipEntryStream.CopyTo(content);
            zipEntryStream.Close();
            content.Seek(0, SeekOrigin.Begin);
            return content;
        }

        private bool IsPkZip(byte[] bytes)
        {
            var zipLeadBytes = 0x04034b50;
            bool isZip = false;
            if (bytes.Length > 4)
            {
                isZip = false;
            }
            else
            {
                isZip = (BitConverter.ToInt32(bytes, 0) == zipLeadBytes);
            }
            return isZip;
        }

        private bool IsGZip(byte[] bytes)
        {
            var gzipLeadBytes = 0x8b1f;
            bool isZip = false;
            if (bytes == null && bytes.Length >= 2)
            {
                isZip = false;
            }
            else
            {
                isZip = (BitConverter.ToUInt16(bytes, 0) == gzipLeadBytes);
            }
            return isZip;
        }

        private bool IsZip(Stream content)
        {
            byte[] bytes = new byte[4];
            content.Read(bytes, 0, 4);
            content.Position = 0;
            return IsPkZip(bytes) || IsGZip(bytes);
        }


    }
}
