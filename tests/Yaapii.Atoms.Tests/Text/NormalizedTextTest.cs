using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class NormalizedTextTest
    {
        [Fact]
        public void NormalizesText()
        {
            Assert.True(
            new NormalizedText(" \t hello  \t\tworld   \t").AsString() == "hello world",
            "Can't normalize a text");
        }
    }
}
