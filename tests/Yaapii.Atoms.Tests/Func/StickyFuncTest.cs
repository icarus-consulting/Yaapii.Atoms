using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Tests.Func
{
    public sealed class StickyFuncTest
    {
        [Fact]
        public void CachesFuncResults()
        {
            IFunc<Boolean, int> func =
                new StickyFunc<bool, int>(
                    input => new Random().Next()
            );

            Assert.True(
                func.Invoke(true) == func.Invoke(true),
                "cannot return function result from cache"
            );
        }
    }
}