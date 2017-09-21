using System;
using System.IO;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.IO
{
    public sealed class ReaderOfTest
    {
        [Fact]
        public void ReadsSimpleFileContent()
        {
            var dir = "artifacts/ReaderOfTest"; var file = "txt-1"; var path = Path.GetFullPath(Path.Combine(dir, file));
            var content = "Hello, товарищ!";
            Directory.CreateDirectory(dir);
            File.Delete(path);

            //Create file through reading source
            new LengthOf(
                new TeeInput(
                    new InputOf(content),
                    new OutputTo(path))
            ).Value();

            Assert.True(
                new TextOf(
                    new InputOf(
                        new ReaderOf(
                            new Uri(path)))
                ).AsString() == content,
                "can't read data from file");
        }

    }
}
