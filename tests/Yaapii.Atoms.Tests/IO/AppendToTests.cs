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
            var temp = Directory.CreateDirectory("artifacts/AppendToTest/");
            var file = Path.GetFullPath(Path.Combine(temp.FullName, "file.txt"));
            if (File.Exists(file)) File.Delete(file);

            var txt = "Hello, товарищ!";

            var lengthOf = new LengthOf(
                new TeeInput(
                    txt,
                    new AppendTo(
                        new OutputTo(file)
                    )));

            lengthOf.Value();
            lengthOf.Value();

            Assert.True(
                new TextOf(
                    new InputAsBytes(
                        new InputOf(new Uri(file))))
                .AsString() == (txt + txt),
                "Can't append path content");
        }

        [Fact]
        public void WritesSimpleFileContent()
        {
            var temp = Directory.CreateDirectory("artifacts/AppendToTest");
            var file = new Uri(Path.GetFullPath(Path.Combine(temp.FullName, "file.txt")));
            if (File.Exists(file.AbsolutePath))
            {
                File.Delete(file.AbsolutePath);
            }

            var txt = "Hello, друг!";
            var lengthOf = new LengthOf(
                new TeeInput(
                    txt,
                    new AppendTo(
                        new OutputTo(file))));

            lengthOf.Value();
            lengthOf.Value();

            Assert.True(
                new TextOf(
                    new InputAsBytes(
                        new InputOf(file)))
                .AsString() == txt + txt,
                "Can't append file content");
        }

        [Fact]
        public void DisposesStream()
        {
            var temp = Directory.CreateDirectory("artifacts/AppendToTest");
            var file = new Uri(Path.GetFullPath(Path.Combine(temp.FullName, "file.txt")));

            var appendTo = new AppendTo(file);

            var stream = appendTo.Stream();
            Assert.True(stream.CanWrite);
            stream.Dispose();
            Assert.False(stream.CanWrite);

            stream = appendTo.Stream();
            Assert.True(stream.CanWrite);
            appendTo.Dispose();
            Assert.False(stream.CanWrite);
        }
    }
}