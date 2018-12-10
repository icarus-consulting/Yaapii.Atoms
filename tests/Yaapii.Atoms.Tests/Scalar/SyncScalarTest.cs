using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class SyncScalarTest
    {
        [Fact]
        public void WorksInMultipleThreads()
        {
            var check = 0;
            var sc = new SyncScalar<int>(() => check += 1);

            var max = Environment.ProcessorCount << 8;
            Parallel.For(0, max, (nr) => sc.Value());

            Assert.Equal(
                max, check
            );
        }
    }
}
