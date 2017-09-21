using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class RepeatedTextTest
    {
        [Fact]
        public void RepeatsWordsText()
        {
            Assert.True(
                new RepeatedText("hello", 2).AsString() == "hellohello",
                "Can't repeat a text");
        }

        [Fact]
        public void RepeatsCharText()
        {
            Assert.True(
                new RepeatedText("A", 5).AsString() == "AAAAA",
                "Can't repeat a char");
        }
    }
}
