using System;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.IO
{
    public sealed class InputStreamOfTest
    {
        public void ReadsSimpleFileContent()
        {
            var dir = "atoms-1"; var file = "txt-1"; var path = Path.GetFullPath(Path.Combine(dir, file));
            Directory.CreateDirectory(dir);
            if (File.Exists(path)) File.Delete(path);

            var temp = File.Create("atoms-1/txt-1");

            String content = "Hello, товарищ!";
            File.WriteAllBytes(path, new BytesOf(new TextOf(content, Encoding.UTF8)).AsBytes());

            Assert.True(
                new TextOf(
                    new InputAsBytes(
                        new InputOf(
                            new InputStreamOf(
                                new Uri(path))))
                ).AsString() == content,
                "Can't read file content");

        }

        [Fact]
        public void ReadsFromReader()
        {
            String content = "Hello, дорогой товарищ!";
            Assert.True(
                new TextOf(
                    new InputOf(
                        new InputStreamOf(
                            new StreamReader(
                                new InputOf(content).Stream())))
                ).AsString() == content);
        }

        [Fact]
        public void ReadsFromReaderThroughSmallBuffer()
        {
            String content = "Hello, صديق!";
            Assert.True(
                new TextOf(
                    new InputOf(
                        new InputStreamOf(
                            new StreamReader(
                                new InputOf(content).Stream()), 
                            1))).AsString() == content,
                "Can't read from reader through small buffer");
        }

        [Fact]
        public void MakesDataAvailable()
        {
            String content = "Hello,חבר!";
            Assert.True(
                new InputStreamOf(content).Length > 0,
                "Can't show that data is available");
        }
    }
}
