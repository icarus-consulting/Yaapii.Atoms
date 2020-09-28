using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class HeadInputStreamTest
    {
        [Fact]
        void TestSkippingLessThanTotal()
        {
            var stream = 
                new HeadInputStream(
                    new InputOf("testSkippingLessThanTotal").Stream(),
                    5
                );

            var skipped = stream.Seek(3L, SeekOrigin.Begin);

            Assert.Equal(
                3L,
                skipped
            );

            Assert.Contains(
                "tS",
                new TextOf(new InputOf(stream)).AsString()
            );
        }

        [Fact]
        void TestSkippingMoreThanTotal()
        {
            var stream = 
                new HeadInputStream(
                    new InputOf("testSkippingMoreThanTotal").Stream(),
                    5
                );
            var skipped = stream.Seek(7L, SeekOrigin.Begin);

            Assert.Equal(
                5L,
                skipped
            );

            var input = new TextOf(stream).AsString();
            Assert.Equal(
                "",
                input
            );
        }
    }
}
