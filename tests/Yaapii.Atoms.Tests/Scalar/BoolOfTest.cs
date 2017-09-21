using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class BoolOfTest
    {
        [Fact]
        public void TrueTest()
        {
            Assert.True(
                new BoolOf("true").Value() == true,
                "Can't parse 'true' string");
        }

        [Fact]
        public void FalseTest()
        {
            Assert.True(
                new BoolOf("false").Value() == false,
                "Can't parse 'false' string"
            );
        }

        [Fact]
        public void IsFalseIfTextDoesNotRepresentABoolean()
        {
            Assert.Throws(
                typeof(IOException),
                () => new BoolOf("abc").Value());
        }
    }
}
