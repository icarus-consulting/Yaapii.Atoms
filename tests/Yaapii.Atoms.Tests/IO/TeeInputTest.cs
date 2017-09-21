using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.IO
{
    public sealed class TeeInputTest
    {
        [Fact]
        public void CopiesContent()
        {
            var baos = new MemoryStream();
            String content = "Hello, товарищ!";
            Assert.True(
                new TextOf(
                    new TeeInput(
                        new InputOf(content),
                        new OutputTo(baos)
                    )
                ).AsString() == Encoding.UTF8.GetString(baos.ToArray()),
                "Can't copy Input to Output and return Input");
        }

        [Fact]
        public void CopiesToFile()
        {
            var dir = "artifacts/TeeInputTest"; var file = "txt.txt"; var path = Path.GetFullPath(Path.Combine(dir, file));
            Directory.CreateDirectory(dir);
            if (File.Exists(path)) File.Delete(path);


            var str = "";
                str = new TextOf(
                    new BytesOf(
                        new TeeInput(
                            "Hello, друг!", 
                            new OutputTo(new Uri(path))))
                ).AsString();

            Assert.True(
                str == new TextOf(new InputOf(new Uri(path))).AsString(),
                "Can't copy Input to File and return content");
        }
    }
}
