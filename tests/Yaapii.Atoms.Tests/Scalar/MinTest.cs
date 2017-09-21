using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Fail;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class MinTest
    {
        [Fact]
        public void MinAmongEmptyTest()
        {
            Assert.Throws(
                typeof(NoSuchElementException),
                () => new Min<int>(new EnumerableOf<int>()).Value());
        }

        [Fact]
        public void MinAmongOneTest()
        {
            int num = 10;
            Assert.True(
                new Min<int>(() => num).Value() == num,
                "Can't find the smaller among one");
        }

        [Fact]
        public void MinAmongManyTest()
        {
            int num = -1;
            Assert.True(
                new Min<int>(
                    () => 1,
                    () => 0,
                    () => num,
                    () => 2
                 ).Value() == num);
        }
    }
}