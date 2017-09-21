using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class IsBlankTest
    {
        [Fact]
        public void DeterminesEmptyText()
        {
            Assert.True(
                new IsBlank(
                    new TextOf("")
                ).Value(),
                "Can't determine an empty text");
        }

        [Fact]
        public void DeterminesBlankText()
        {
            Assert.True(
                new IsBlank(
                    new TextOf("  ")
                ).Value(),
                "Can't determine an empty text with spaces");
        }

        [Fact]
        public void determinesNotBlankText()
        {
            Assert.False(
                new IsBlank(
                    new TextOf("not empty")
                ).Value(),
                "Can't detect a nonempty text");
        }
    }
}