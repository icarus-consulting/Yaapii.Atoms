using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.IO
{
    public sealed class ReaderAsBytesTest
    {
        [Fact]
        public void ReadsString()
        {
            String source = "hello, друг!";
            Assert.True(
            new TextOf(
                new ReaderAsBytes(
                    new StreamReader(
                        new InputOf(source).Stream())
                )
            ).AsString() == source);
        }
    }
}
