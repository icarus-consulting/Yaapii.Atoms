using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class FloatOfTest
    {
        [Fact]
        public void NumberTest()
        {
            Assert.True(
            new FloatOf("1656.894").Value() == 1656.894F);
        }

        [Fact]
        public void FailsIfTextDoesNotRepresentAFloat()
        {
            Assert.Throws(
                typeof(FormatException),
                () => new FloatOf("abc").Value());
        }
    }
}