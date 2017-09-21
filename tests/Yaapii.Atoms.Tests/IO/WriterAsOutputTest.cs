using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.IO;
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
