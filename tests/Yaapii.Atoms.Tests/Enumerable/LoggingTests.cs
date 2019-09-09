using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Tests.Enumerable
{
    public sealed class LoggingTests
    {
        [Fact]
        public void DumpsItemsOnRead()
        {
            string result = string.Empty;

            new LengthOf(
                new Logging<string>(
                    new List<string>() { "A", "B" },
                    item => result += item
                )
            ).Value();

            Assert.Equal("AB", result);
        }
    }
}
