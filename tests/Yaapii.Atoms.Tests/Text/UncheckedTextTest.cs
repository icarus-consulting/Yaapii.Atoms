using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class UncheckedTextTest
    {

        [Fact]
        public void rethrowsCheckedToUncheckedException()
        {
            Assert.Throws(
                typeof(UncheckedIOException),
                () => new UncheckedText(
                    new FailingText()).AsString());
        }

        [Fact]
        public void ComparesToOtherUncheckedText()
        {
            String txt = "foobar";
            Assert.True(
                new UncheckedText(
                    new TextOf(txt)
                ).CompareTo(new TextOf(txt)) == 0,
                "These UncheckedText are not equal");
        }

        class FailingText : IText
        {
            public string AsString()
            {
                throw new IOException("intended");
            }

            public bool Equals(IText other)
            {
                throw new UnsupportedOperationException("#Equals() not supported");
            }
        }

    }
}
