using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Tests.Enumerable
{
    public sealed class CycledTest
    {
        [Fact]
        public void RepeatEnumerableTest()
        {
            string expected = "two";
            Assert.True(
                new ItemAt<string>(
                    new Cycled<string>(
                        new EnumerableOf<string>(
                            "one", expected, "three"
                            )), 
                    7
                    ).Value() == expected);
        }
    }
}
