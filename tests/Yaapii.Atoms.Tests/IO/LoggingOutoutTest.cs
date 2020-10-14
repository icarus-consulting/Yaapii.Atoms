using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.IO;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class LoggingOutoutTest
    {
        [Fact]
        public void logWriteZero()
        {
            var res =
                new LengthOf(
                    new TeeInput(
                        new InputOf(""),
                        new LoggingOutput(
                            new ConsoleOutput(),
                            "memory"
                        )
                    )
                ).Value();

            Assert.Equal(
                0L,
                res
            );
        }

        [Fact]
        public void LogWriteOneByte()
        {

            var output = 
                new LoggingOutput(
                    new ConsoleOutput(),
                    "memory"
                ).Stream();

            output.Write(new BytesOf("a").AsBytes(), 0, 1);


        }

        [Fact]
        public void LogWriteText()
        {
            var output =
                new LoggingOutput(
                    new ConsoleOutput(),
                    "memory"
                ).Stream();

            var bytes = new BytesOf("Hello, товарищ!").AsBytes();
            output.Write(bytes,0, bytes.Length);
           
            new Assertion<>(
                "Can't log 22 bytes written to memory",
                logger.toString(),
                Matchers.containsString("Written 22 byte(s) to memory in")
            ).affirm();
        }
/*
    @Test
    public void logWriteToLargeTextFile() throws Exception
{
    final Logger logger = new FakeLogger();
final Path temp = this.folder.newFolder("ccts-1").toPath();
final Path path = temp.resolve("x/y/z/file.txt");
try (OutputStream output = new LoggingOutput(
    new OutputTo(path),
    "text file",
    logger
).stream()
        ) {
    new LengthOf(
        new TeeInput(
            new ResourceOf("org/cactoos/large-text.txt"),
            new OutputTo(output)
        )
    ).intValue();
}
new Assertion<>(
    "Can't log write and close operations to text file",
    logger.toString(),
    Matchers.allOf(
        Matchers.not(
            Matchers.containsString(
                "Written 16384 byte(s) to text file"
            )
        ),
        Matchers.containsString("Written 74536 byte(s) to text file"),
        Matchers.containsString("Closed output stream from text file")
    )
).affirm();
}

    @Test
    public void logAllWriteToLargeTextFile() throws Exception
{
    final Logger logger = new FakeLogger(Level.WARNING);
final Path temp = this.folder.newFolder("ccts-2").toPath();
final Path path = temp.resolve("a/b/c/file.txt");
try (OutputStream output = new LoggingOutput(
    new OutputTo(path),
    "text file",
    logger
).stream()
        ) {
    new LengthOf(
        new TeeInput(
            new ResourceOf("org/cactoos/large-text.txt"),
            new OutputTo(output)
        )
    ).intValue();
}
new Assertion<>(
    "Can't log all write and close operations to text file",
    logger.toString(),
    Matchers.allOf(
        Matchers.containsString("Written 16384 byte(s) to text file"),
        Matchers.containsString("Written 32768 byte(s) to text file"),
        Matchers.containsString("Written 49152 byte(s) to text file"),
        Matchers.containsString("Written 65536 byte(s) to text file"),
        Matchers.containsString("Written 74536 byte(s) to text file"),
        Matchers.containsString("Closed output stream from text file")
    )
).affirm();
}*/
    }
}
