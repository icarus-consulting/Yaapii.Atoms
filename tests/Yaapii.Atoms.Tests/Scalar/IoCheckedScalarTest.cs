using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class IoCheckedScalarTest
    {
        [Fact]
        public void RethrowsCheckedToUncheckedException()
        {
            var exception = new IOException("intended");
            try
            {
                new IoCheckedScalar<int>(
                    new ScalarOf<int>(
                    () => throw exception)
                ).Value();
            }
            catch (IOException ex)
            {
                Assert.True(ex == exception);
            }
        }

    }
}
