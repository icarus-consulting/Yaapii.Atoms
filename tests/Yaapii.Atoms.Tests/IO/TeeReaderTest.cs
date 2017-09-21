using System.IO;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.IO
{
    public sealed class TeeReaderTest
    {
        [Fact]
        public void TestTeeReader()
        {
            var baos = new MemoryStream();
            var content = "Hello, товарищ!";

            var reader = new TeeReader(
                new ReaderOf(content),
                new WriterTo(
                    new OutputTo(baos))
            );
            int done = 0;
            while (done >= 0)
            {
                done = reader.Read();
            }
            reader.Dispose();
            Assert.True(
                new TextOf(
                    new InputOf(
                        new ReaderOf(baos.ToArray()))
                ).AsString().CompareTo(content) == 0,
                "Can't read content");
        }

    }
}
