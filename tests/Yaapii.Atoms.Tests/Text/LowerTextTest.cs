using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class LowerTextTest
    {
        [Fact]
        public void ConvertsText()
        {
            Assert.True(
                new LowerText(
                    new TextOf("HelLo!")).AsString() == "hello!",
                "Can't lower case a text");
        }
    }
}