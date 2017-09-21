using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class EndlessTest
    {
        [Fact]
        public void EndlessIterableTest()
        {
            Assert.True(
                new ItemAt<int>(
                    new Endless<int>(1),
                    0).Value() == 1,
                "Can't get unique endless iterable item");
        }
    }
}
