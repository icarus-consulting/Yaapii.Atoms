using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class DoubleOfTest
    {
        [Fact]
        public void NumberTest()
        {
            Assert.True(
                new DoubleOf("185.65156465123").Value() == 185.65156465123,
                "Can't parse double number");
        }

        [Fact]
        public void FailsIfTextDoesNotRepresentADouble()
        {
            Assert.Throws(typeof(FormatException),
            () => new DoubleOf("abc").Value());
        }
    }
}
