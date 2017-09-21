using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.IO
{
    public sealed class OutputToTest
    {
        [Fact]
        public void WritesSimplePathContent()
        {
            var temp = Directory.CreateDirectory("artifacts/OutputToTest/");
            var file = Path.GetFullPath(Path.Combine(temp.FullName, "file.txt"));
            if (File.Exists(file))File.Delete(file);

            String content = "Hello, товарищ!";
            new LengthOf(
                new TeeInput(
                    content, 
                    new OutputTo(file))
                ).Value();

            Assert.True(
                new TextOf(
                    new InputAsBytes(
                        new InputOf(new Uri(file))))
                .AsString() == content,
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

            String txt = "Hello, друг!";
            new LengthOf(
                new TeeInput(
                    txt, 
                    new OutputTo(file))
            ).Value();

            Assert.True(
                new TextOf(
                    new InputAsBytes(
                        new InputOf(file)))
                .AsString() == txt,
                "Can't write file content");
    }
}
}
