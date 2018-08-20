using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class SolidScalarTest
    {
        [Fact]
        public void CachesResult()
        {
            var check = 0;
            var sc = new SolidScalar<int>(() => check += 1);
            var max = Environment.ProcessorCount << 8;
            Parallel.For(0, max, (nr) => sc.Value());

            Assert.Equal(sc.Value(), sc.Value());
        }

        [Fact]
        public void WorksInMultipleThreads()
        {
            var check = 0;
            var sc = new SolidScalar<IList<int>>(() => new ListOf<int>(1, 2));
            var max = Environment.ProcessorCount << 8;
            Parallel.For(0, max, (nr) => sc.Value());

            Assert.Equal(
                sc.Value(), sc.Value()
            );
        }
    }
}
