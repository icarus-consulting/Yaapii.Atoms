using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Tests.Func
{
    public sealed class UncheckedBiFuncTest
    {

        public void RethrowsCheckedToUncheckedException()
        {
            Assert.Throws(
                typeof(UncheckedIOException),
                    () => new UncheckedBiFunc<int, int, bool>(
                        (fst, scd) =>
                        {
                            throw new IOException("intended");
                        }
                    ).Apply(1, 2));
        }

        [Fact]
        public void TestUncheckedBiFunc()
        {
            Assert.True(
                new UncheckedBiFunc<int, int, bool>(
                    (fst, scd) => true
                ).Apply(1, 2) == true);

        }
    }
}
