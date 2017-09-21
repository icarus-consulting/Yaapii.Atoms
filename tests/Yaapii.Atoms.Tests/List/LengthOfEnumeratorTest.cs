using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class LengthOfEnumeratorTest
    {
        [Fact]
        public void Counts()
        {
            Assert.True(
                new LengthOfEnumerator<int>(
                    new EnumerableOf<int>(1, 2, 3, 4, 5).GetEnumerator()).Value() == 5,
                "cannot count items");
        }
    }
}
