using System.Collections.Generic;
using Xunit;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Scalar.Tests
{
    public sealed class EachTests
    {
        [Fact]
        public void IncreasesOne()
        {
            List<int> lst = new List<int>() { 2, 1, 0};

            new Each<int>(
                (i) => lst[i] = i, 
                0,1,2
            ).Invoke();

            Assert.True(
                lst[0] == 0 && 
                lst[2] == 2
            );
        }

        [Fact]
        public void TestProc()
        {
            var list = new LinkedList<int>();
            new Each<int>(
                new ActionOf<int>(i => list.AddLast(i)),
                1, 1
            ).Invoke();

            Assert.True(
                list.Count == 2);
        }
    }
}
