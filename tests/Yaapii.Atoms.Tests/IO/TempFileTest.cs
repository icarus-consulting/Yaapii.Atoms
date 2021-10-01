// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
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
using Xunit;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class TempFileTest
    {

        [Fact]
        public void CreateAndDeleteTemporyFile()
        {
            var file = new TempFile();
            using (file)
            {
                Assert.True(File.Exists(file.Value()));
            }
            Assert.False(File.Exists(file.Value()));
        }


        [Theory]
        [InlineData("txt")]
        [InlineData(".txt")]
        public void CreateAndDeleteTemporyFileWithGivenExtension(string extension)
        {
            var file = new TempFile(extension);
            using (file)
            {
                Assert.True(File.Exists(file.Value()));
            }
            Assert.False(File.Exists(file.Value()));
        }

        [Fact]
        public void CreateAndDeleteTemporyFileByFileInfo()
        {
            var file = Path.GetTempFileName();
            using (var tmpFile = new TempFile(new FileInfo(file)))
            {
                Assert.True(File.Exists(tmpFile.Value()));
            }
            Assert.False(File.Exists(file));
        }
    }
}
