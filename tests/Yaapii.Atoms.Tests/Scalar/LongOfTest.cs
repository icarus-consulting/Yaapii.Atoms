using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class LongOfTest
    {
        [Fact]
        public void NumberTest()
        {
            Assert.True(
                new LongOf("186789235425346").Value() == 186789235425346L);
        }

        [Fact]
        public void FailsIfTextDoesNotRepresentALong()
        {
            Assert.Throws(
                typeof(FormatException),
                () => new LongOf("abc").Value());
        }
    }
}
