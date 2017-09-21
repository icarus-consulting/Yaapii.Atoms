using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Tests.Func
{
    public sealed class StickyBiFuncTest
    {
        [Fact]
        public void CachesFuncResults()
        {
            var func = new StickyBiFunc<bool, bool, Int32>(
                (first, second) => new Random().Next()
            );

            Assert.True(
                func.Apply(true, true) + func.Apply(true, true)
                == func.Apply(true, true) + func.Apply(true, true),
                "cannot cache results");
        }
    }
}