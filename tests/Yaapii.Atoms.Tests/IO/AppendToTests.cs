using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class AppendToTests
    {
        [Fact]
        public void WritesSimplePathContent()
        {
            var temp = Directory.CreateDirectory("artifacts/OutputToTest/");
            var file = Path.GetFullPath(Path.Combine(temp.FullName, "file.txt"));
            if (File.Exists(file)) File.Delete(file);

            var content = "Hello, товарищ!";
            var content2 = "Bye,  товарищ!";
            new LengthOf(
                new TeeInput(
                    content,
                    new OutputTo(file))
                ).Value();

            new LengthOf(
                new TeeInput(
                    content2,
                    new AppendTo(file))
                ).Value();

            Assert.True(
                new TextOf(
                    new InputAsBytes(
                        new InputOf(new Uri(file))))
                .AsString() == (content+content2),
                "Can't write path content");
        }

        [Fact]
        public void WritesSimpleFileContent()
        {
            var temp = Directory.CreateDirectory("artifacts/OutputToTest");
            var file = new Uri(Path.GetFullPath(Path.Combine(temp.FullName, "file.txt")));
            if (File.Exists(file.AbsolutePath))
            {
                File.Delete(file.AbsolutePath);
            }

            var txt = "Hello, друг!";
            var txt2 = "Bye, друг!";
            new LengthOf(
                new TeeInput(
                    txt,
                    new OutputTo(file))
            ).Value();

            new LengthOf(
                new TeeInput(
                    txt2,
                    new AppendTo(file))
                ).Value();

            Assert.True(
                new TextOf(
                    new InputAsBytes(
                        new InputOf(file)))
                .AsString() == txt+txt2,
                "Can't write file content");
        }
    }
}
