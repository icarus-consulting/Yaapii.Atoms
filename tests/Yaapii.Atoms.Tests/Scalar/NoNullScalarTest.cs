using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Scalar
{
    public class NoNullScalarTest
    {
        [Fact]
        public void RaisesError()
        {
            Assert.Throws<IOException>(
                () =>
                new NoNullScalar<string>(null).Value());
        }

        [Fact]
        public void GivesFallback()
        {
            var fbk = "Here, take this instead";
            string val = null;
            Assert.True(
                new NoNullScalar<string>(val, fbk).Value() == fbk,
                "can't get fallback value");
        }
    }
}
