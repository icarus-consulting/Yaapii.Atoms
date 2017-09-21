using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Tests.Func
{
    public sealed class IoCheckedBiFuncTest
    {
        [Fact]
        public void RethrowsCheckedToUncheckedException()
        {
            IOException exception = new IOException("intended");
            try
            {
                new IoCheckedBiFunc<int, int, int>(
                    (fst, scd) =>
                    {
                        throw exception;
                    }
            ).Apply(1, 2);
            }
            catch (IOException ex)
            {
                Assert.True(ex.GetType() == exception.GetType());
            }
        }

        [Fact]
        public void ThrowsException()
        {
            Assert.Throws(
                typeof(IOException),
                () => new IoCheckedBiFunc<int, int, int>(
                    (fst, scd) =>
                    {
                        throw new Exception();
                    }
                ).Apply(1, 2));
        }
    }
}
