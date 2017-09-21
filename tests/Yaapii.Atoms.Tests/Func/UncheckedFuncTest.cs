using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Tests.Func
{
    public sealed class UncheckedFuncTest
    {
        [Fact]
        public void RethrowsCheckedToUncheckedException()
        {
            Assert.Throws(
                typeof(UncheckedIOException), () =>
                new UncheckedFunc<int, string>(
                    i => throw new IOException("intended")
                ).Invoke(1));
        }
    }
}
