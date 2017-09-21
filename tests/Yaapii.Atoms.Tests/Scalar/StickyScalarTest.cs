using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class StickyScalarTest
    {
        [Fact]
        public void CachesScalarResults()
        {
            IScalar<int> scalar =
                new StickyScalar<int>(
                    new ScalarOf<int>(() => new Random().Next()));

            Assert.True(scalar.Value() == scalar.Value(),
                "cannot return value from cache"
            );
        }
    }
}
