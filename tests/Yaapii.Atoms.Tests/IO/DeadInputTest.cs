using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.IO
{
    public sealed class DeadInputTest
    {
        [Fact]
        public void ReadsEmptyContent()
        {
            Assert.True(
                new TextOf(
                    new DeadInput())
                .AsString() == "",
                "Can't read empty content");
        }

    }
}
