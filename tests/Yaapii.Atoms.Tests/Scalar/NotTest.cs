using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class NotTest
    {
        [Fact]
        public void TrueToFalse()
        {
            Assert.True(
            new Not(new True()).Value() == new False().Value());
        }

        [Fact]
        public void FalseToTrue()
        {
            Assert.True(
                new Not(new False()).Value() == new True().Value());
        }
    }
}
