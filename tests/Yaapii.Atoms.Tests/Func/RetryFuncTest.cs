using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Tests.Func
{
    public sealed class RetryFuncTest
    {
        [Fact]
        public void RunsFuncMultipleTimes()
        {
            Assert.True(
            new RetryFunc<bool, int>(
                input =>
                {
                    if (new Random().NextDouble() > 0.3d)
                    {
                        throw new ArgumentException("May happen");
                    }
                    return 0;
                },
                int.MaxValue
            ).Invoke(true) == 0,
            "cannot retry function"
            );
        }
    }
}
