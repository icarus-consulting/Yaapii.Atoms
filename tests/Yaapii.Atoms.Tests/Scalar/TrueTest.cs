using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class TrueTest
    {
        [Fact]
        public void AsValue()
        {
            Assert.True(
            new True().Value() == true);

        }
    }
}
