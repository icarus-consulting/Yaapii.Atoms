using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Primitives
{
    public sealed class DoubleOfTests
    {
        [Fact]
        public void ConvertsDouble()
        {
            var piStr = Math.PI.ToString("G17");
            var piFromStr = new DoubleOf(piStr, CultureInfo.CurrentCulture).Value();
            var piStr2 = piFromStr.ToString("G17");

            Assert.True(piStr == piStr2);
        }
    }
}
