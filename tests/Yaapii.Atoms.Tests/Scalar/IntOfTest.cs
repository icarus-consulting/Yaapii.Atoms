using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class IntOfTest
    {
        [Fact]
        public void NumberTest()
        {
            Assert.True(
            new IntOf("1867892354").Value() == 1867892354,
            "Can't parse integer number");
        }

        [Fact]
        public void FailsIfTextDoesNotRepresentAnInt()
        {
            Assert.Throws(
                typeof(FormatException),
                () => new DoubleOf("abc").Value());

        }
    }
}
