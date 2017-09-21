using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class TrimmedRightTextTest
    {
        [Fact]
    public void convertsText()
        {
            Assert.True(
                new TrimmedRightText(new TextOf("  Hello!   \t ")).AsString() == "  Hello!",
                "Can't right trim a text");
        }

        [Fact]
        public void TrimmedBlankTextIsEmptyText()
        {
            Assert.True(
                new TrimmedRightText(new TextOf("  \t ")).AsString() == "",
                "Can't trim a blank text");
        }
    }
}
