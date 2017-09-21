using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class UncheckedScalarTest
    {
        [Fact]
        public void RethrowsCheckedToUncheckedException()
        {
            Assert.Throws(
                typeof(UncheckedIOException),
                () => new UncheckedScalar<int>(
                    () =>
                    {
                        throw new IOException("intended");
                    }).Value());
        }
    }
}
