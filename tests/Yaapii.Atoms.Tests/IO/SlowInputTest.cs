using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.IO
{
    public sealed class SlowInputTest
    {
        [Fact]
        public void CalculatesLength()
        {
            String text = "What's up, друг?";
            Assert.True(
                new LengthOf(
                    new SlowInput(
                        new InputOf(
                            new TextOf(text)))
                ).Value() == Encoding.UTF8.GetBytes(text).Length,
                "Can't calculate the length of Input");
        }

        [Fact]
        public void ReadsFileContentSlowly()
        {
            long size = 100_000L;
            Assert.True(
                new LengthOf(
                    new SlowInput(size)
                ).Value() == size,
            "Can't calculate length if the input is slow");
        }

    }

}
