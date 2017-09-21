using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class ReversedTextTest
    {
        [Fact]
        public void ReverseText()
        {
            Assert.True(
                new ReversedText(
                    new TextOf("Hello!")
                ).AsString() == "!olleH",
                "Can't reverse a text");
        }

        [Fact]
        public void ReversedEmptyTextIsEmptyText()
        {
            Assert.True(
                new ReversedText(
                    new TextOf("")
                ).AsString() == "",
                "Can't reverse empty text");
        }
    }
}
