using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class UpperTextTest
    {
        [Fact]
        public void convertsText()
        {
            Assert.True(
                new UpperText(new TextOf("Hello!")).AsString() == "HELLO!",
                "Can't upper case a text");
        }

    }
}
