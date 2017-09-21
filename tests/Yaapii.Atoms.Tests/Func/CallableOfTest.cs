using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Tests.Func
{
    public sealed class CallableOfTest
    {
        [Fact]
        public void ConvertsFuncIntoCallable()
        {
            Assert.True(
            new CallableOf<int>(
                () => 1
            ).Call() == 1,
            "cannot convert func into callable");
        }
    }
}
