/// MIT License
///
/// Copyright(c) 2017 ICARUS Consulting GmbH
///
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
///
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
///
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.

using System;
using System.IO;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.IO
{
    public sealed class WriterAsOutputTest
    {
        [Fact]
        public void WritesLargeContentToFile()
        {
            var dir = "artifacts/WriterAsOutputTest"; var inputFile = "large-text.txt";
            var inputPath = Path.GetFullPath(Path.Combine(dir, inputFile));
            var outputFile = "text-copy.txt";
            var outputPath = Path.GetFullPath(Path.Combine(dir, outputFile));

            Directory.CreateDirectory(dir);
            if (File.Exists(inputPath)) File.Delete(inputPath);
            if (File.Exists(outputPath)) File.Delete(outputPath);

            //Create large file
            new LengthOf(
                new InputOf(
                    new TeeInputStream(
                        new MemoryStream(
                            new BytesOf(
                                new JoinedText(",",
                                new Limited<string>(
                                    new Endless<string>("Hello World"),
                                    1000))
                                ).AsBytes()),
                        new OutputTo(
                            new Uri(inputPath)).Stream()))
            ).Value();

            //Read from large file and write to output file (make a copy)
            long left;
            left = new LengthOf(
                    new TeeInput(
                        new InputOf(
                            new Uri(Path.GetFullPath(inputPath))),
                        new WriterAsOutput(
                            new StreamWriter(new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))))
                    ).Value();

            long right = new LengthOf(
                            new InputOf(
                                new Uri(Path.GetFullPath(outputPath)))
                         ).Value();

            Assert.True(left == right, "input and output are not the same size");
        }

    }
}
