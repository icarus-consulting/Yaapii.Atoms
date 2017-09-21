using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.IO
{
    public sealed class DeadOutputTest
    {
        [Fact]
        public void ReadsEmptyContent()
        {
            Assert.True(
                new TextOf(
                new TeeInput(
                    new InputOf("How are you, мой друг?"),
                    new DeadOutput()
                )).AsString().EndsWith("друг?"));
        }
    }
}
