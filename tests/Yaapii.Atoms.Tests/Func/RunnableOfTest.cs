using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Tests.Func
{
    public sealed class RunnableOfTest
    {
        [Fact]
        public void ConvertsFuncIntoRunnable()
        {
            var i = 0;

            new RunnableOf<int>(
                input =>
                {
                    i = input;
                },
                1
            ).Run();

            Assert.True(i == 1,
                "cannot convert func to runnable");
        }
    }
}
