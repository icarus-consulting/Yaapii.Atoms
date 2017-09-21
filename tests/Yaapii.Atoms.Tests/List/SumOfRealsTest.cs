using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class SumOfRealsTest
    {

        [Fact]
        public void WithVarargsCtor()
        {
            Assert.True(
                Math.Round(new SumOfReals(1.2, 2.5, 3.3).Value(), 1) == 7.0);
        }

        [Fact]
        public void WithIterCtor()
        {
            Assert.True(
                Math.Round(
            new SumOfReals(
                new EnumerableOf<IScalar<Double>>(
                    new ScalarOf<Double>(7.1),
                    new ScalarOf<Double>(8.1),
                    new ScalarOf<Double>(10.1))
                ).Value(),1) == 25.3);
        }
    }
}
