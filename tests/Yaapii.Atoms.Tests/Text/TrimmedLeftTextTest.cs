using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class TrimmedLeftTextTest
    {
        [Fact]
        public void ConvertsText()
        {
            Assert.True(
                new TrimmedLeftText(new TextOf("  Hello!   \t ")).AsString() == "Hello!   \t ",
                "Can't trim text");
        }

        [Fact]
        public void TrimmedBlankTextIsEmptyText()
        {
            Assert.True(
                new TrimmedLeftText(new TextOf("  \t ")).AsString() == "",
                "Can't trim a blank text");
        }
    }
}
