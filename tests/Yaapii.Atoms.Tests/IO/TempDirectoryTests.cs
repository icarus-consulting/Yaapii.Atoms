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
using Xunit;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class TempDirectoryTest
    {
        [Fact]
        public void CreatesDirectory()
        {
            var path = Path.Combine(Path.GetTempPath(), "SunnyDirectory");
            try
            {
                var td = new TempDirectory(path).Value();
                Assert.True(
                    Directory.Exists(path)
                );
            }
            finally
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
        }

        [Fact]
        public void CreatesRandomDirectory()
        {
            string path = String.Empty;
            try
            {
                using (var td = new TempDirectory())
                {
                    path = td.Value().FullName;
                    Assert.True(
                        Directory.Exists(path)
                    );
                }
            }
            finally
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
        }

        [Fact]
        public void DeletesEmptyDirectory()
        {
            var path = Path.Combine(Path.GetTempPath(), "SunnyDirectory");
            try
            {
                using (var td = new TempDirectory(path))
                {
                    td.Value();
                }
                Assert.False(
                    Directory.Exists(path)
                );
            }
            finally
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
        }

        [Fact]
        public void DeletesNotEmptyDirectory()
        {
            var path = Path.Combine(Path.GetTempPath(), "SunnyDirectory");
            using (var td = new TempDirectory(path))
            {
                td.Value();
                File.WriteAllBytes(Path.Combine(path, "raining.txt"), new byte[] { 0xAB });
            }
            try
            {
                Assert.False(
                    Directory.Exists(path)
                );
            }
            finally
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
        }

        [Fact]
        public void DeletesWhenWriteProtectedFilesInside()
        {
            var tempPath = Path.Combine(Path.GetTempPath(), "SunnyDirectory");
            using (var td = new TempDirectory(tempPath))
            {
                td.Value();
                var file = Path.Combine(tempPath, "raining.txt");
                File.WriteAllBytes(file, new byte[] { 0xAB });
                new FileInfo(file).Attributes = FileAttributes.ReadOnly;
            }
            try
            {
                Assert.False(
                    Directory.Exists(tempPath)
                );
            }
            finally
            {
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }
            }
        }
    }
}
