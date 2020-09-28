// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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

using System.IO;
using System.IO.Compression;
using Xunit;

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

                var streamOfZipped = new Zip(folder);

                var archive = new ZipArchive(streamOfZipped.Stream());
                Assert.True(archive.GetEntry(Path.Combine(folder, "FileToZipTwo.txt")) != null);
            }
            finally
            {
                Directory.Delete(folder, true);
            }
        }
    }
}
