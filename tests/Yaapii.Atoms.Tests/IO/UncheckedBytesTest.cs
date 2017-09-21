using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.IO
{
    public sealed class UncheckedBytesTest
    {
        [Fact]
        public void RethrowsCheckedToUncheckedException()
        {
            Assert.Throws(typeof(UncheckedIOException),
                () =>
                    new UncheckedBytes(
                        new BytesOf(
                            new InputOf(
                                new Uri(Path.GetFullPath("does-not-exist-for-sure")))
                        ),
                        (ex) => throw new IOException("intended")
                    ).AsBytes());
        }

    }
}
