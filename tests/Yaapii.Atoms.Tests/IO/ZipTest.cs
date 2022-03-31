// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
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
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO.Tests
{
    public class ZipTest
    {
        [Fact]
        public void HasData()
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "ZipTest");
            try
            {
                Directory.CreateDirectory(folder);
                var newFile = File.Create(folder + "\\FileToZipOne.txt");
                newFile.Close();
                newFile = File.Create(folder + "\\FileToZipTwo.txt");
                newFile.Close();
                newFile = File.Create(folder + "\\FileToZipThree.txt");
                newFile.Close();

                var archive = new Zip(folder);
                Assert.InRange<long>(archive.Stream().Length, 1, long.MaxValue);

            }
            finally
            {
                Directory.Delete(folder, true);
            }
}

        [Fact]
        public void HasEntry()
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "ZipTest");
            try
            {
                Directory.CreateDirectory(folder);
                var newFile = File.Create(Path.Combine(folder, "FileToZipOne.txt"));
                newFile.Close();
                newFile = File.Create(Path.Combine(folder, "FileToZipTwo.txt"));
                newFile.Close();
                newFile = File.Create(Path.Combine(folder, "FileToZipThree.txt"));
                newFile.Close();

                var streamOfZipped = new Zip(folder).Stream();

                var archive = new ZipArchive(streamOfZipped);
                Assert.True(archive.GetEntry("FileToZipTwo.txt") != null);
            }
            finally
            {
                Directory.Delete(folder, true);
            }
        }   

        [Fact]
        public void ZipSomeFiles()
        {
            var folder = new TempDirectory("abc\\data").Value();
            var firstInput = Path.GetFullPath(Path.Combine(folder.FullName, "A.txt"));
            var secondInput = Path.GetFullPath(Path.Combine(folder.FullName, "B.txt"));
            var thirdInput = Path.GetFullPath(Path.Combine(folder.FullName, "C.txt"));
            var zipPath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "abc", "data-compressed.zip"));
            var zipPathCopy = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "abc", "data-compressedCopy.zip"));

            if (File.Exists(firstInput)) File.Delete(firstInput);
            if (File.Exists(secondInput)) File.Delete(secondInput);
            if (File.Exists(thirdInput)) File.Delete(thirdInput);
            if (File.Exists(zipPath)) File.Delete(zipPath);
            if (File.Exists(zipPathCopy)) File.Delete(zipPathCopy);

            new LengthOf(
                new TeeInput(
                    new InputOf("A123"),
                    new OutputTo(firstInput)
                )
            ).Value();

            new LengthOf(
                new TeeInput(
                    new InputOf("B456"),
                    new OutputTo(secondInput)
                )
            ).Value();

            new LengthOf(
                new TeeInput(
                    new InputOf("C789"),
                    new OutputTo(thirdInput)
                )
            ).Value();


            using (var stream = new Zip(folder.FullName).Stream())
            {
                using (var fileStream = new FileStream(zipPath, FileMode.Create))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.CopyTo(fileStream);
                }
                Assert.True(new ZipArchive(stream).GetEntry("A.txt") != null);
                Assert.True(new ZipArchive(stream).GetEntry("B.txt") != null);
                Assert.True(new ZipArchive(stream).GetEntry("C.txt") != null);
            }

            using (var stream = new Zip(folder.FullName).Stream())
            {
                using (var fileStream = new FileStream(zipPathCopy, FileMode.Create))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
                Assert.True(new ZipArchive(stream).GetEntry("A.txt") != null);
                Assert.True(new ZipArchive(stream).GetEntry("B.txt") != null);
                Assert.True(new ZipArchive(stream).GetEntry("C.txt") != null);
            }
        }
    }
}
