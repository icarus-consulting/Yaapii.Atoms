using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Fail;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class MaxTest
    {
        [Fact]
        public void MaxAmongEmptyTest()
        {
            Assert.Throws(
                typeof(NoSuchElementException),
                () => new Max<int>(new EnumerableOf<int>()).Value());
        }

        [Fact]
        public void MaxAmongOneTest()
        {
            int num = 10;
            Assert.True(
                new Max<int>(() => num).Value() == num,
                "Can't find the greater among one"
            );
        }

        [Fact]
        public void MaxAmongManyTest()
        {
            int num = 10;
            Assert.True(
                new Max<int>(
                    () => num,
                    () => 0,
                    () => -1,
                    () => 2
                 ).Value() == num,
                "Can't find the greater among many");
        }
    }
}
