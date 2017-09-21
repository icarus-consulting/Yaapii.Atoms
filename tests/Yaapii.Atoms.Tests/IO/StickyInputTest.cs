using System;
using System.IO;
using Xunit;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.IO
{
    public sealed class StickyInputTest
    {
        [Fact]
        public void ReadsFileContent()
        {
            var dir = "artifacts/StickyInputTest"; var file = "large-text.txt"; var path = Path.GetFullPath(Path.Combine(dir, file));
            Directory.CreateDirectory(dir);
            if (File.Exists(path)) File.Delete(path);

            var str = "Hello World"; var lmt = "\r\n"; var times = 1000;

            var length = 
                new LengthOf(
                    new InputOf(
                        new TeeInputStream(
                            new MemoryStream(
                                new BytesOf(
                                    new JoinedText(lmt,
                                    new Limited<string>(
                                        new Endless<string>(str),
                                        times))
                                    ).AsBytes()),
                            new OutputTo(
                                new Uri(path)).Stream()))
                ).Value();

            var ipt = new StickyInput(new InputOf(new Uri(path)));

            Assert.True(
                new RepeatedFunc<IInput, Boolean>(
                    input => new BytesOf(input).AsBytes().Length == length,
                    10
                ).Invoke(ipt),
                "can't return the same result every time");
        }

        [Fact]
        public void ReadsRealUrl()
        {
            Assert.True(
                new TextOf(
                        new StickyInput(
                            new InputOf(
                                new Url("http://www.google.de")))
                ).AsString().Contains("<html"),
            "Can't fetch text page from the URL");
        }

        [Fact]
        public void ReadsFileContentSlowlyAndCountsLength()
        {
            long size = 100_000L;
            Assert.True(
                new LengthOf(
                    new StickyInput(
                        new SlowInput(size)
                    )
                ).Value() == size,
                "Can't read bytes from a large source slowly and count length");
        }

        [Fact]
        public void ReadsFileContentSlowly()
        {
            int size = 130_000;
            Assert.True(
                new BytesOf(
                    new StickyInput(
                        new SlowInput(size)
                    )
                ).AsBytes().Length == size,
                "Can't read bytes from a large source slowly");
        }
    }
}
