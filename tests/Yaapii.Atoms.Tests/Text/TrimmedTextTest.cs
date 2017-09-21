using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class TrimmedTextTest
    {
        [Fact]
        public void ConvertsText()
        {
            Assert.True(
                new TrimmedText(new TextOf("  Hello!   \t ")).AsString() == "Hello!",
                "Can't trim a text");
        }

        [Fact]
        public void TrimmedBlankTextIsEmptyText()
        {
            Assert.True(
                new TrimmedText(new TextOf("  \t ")).AsString() == "",
                "Can't trim a blank text");
        }
    }
}
