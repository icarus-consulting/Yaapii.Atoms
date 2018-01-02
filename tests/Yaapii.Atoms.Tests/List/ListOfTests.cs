using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.List.Tests
{
    public sealed class ListOfTest
    {
        [Fact]
        public void BehavesAsList()
        {
            Assert.True(
                new ListOf<int>(1, 2).Contains(2),
                "Can't behave as a list");
        }

        [Fact]
        public void ElementAtIndexTest()
        {
            int num = 345;

            Assert.True(
                new ListOf<int>(-1, num, 0, 1)[1] == num,
                "Can't convert an iterable to a list");
        }

        [Fact]
        public void EmptyTest()
        {
            Assert.True(
            new ListOf<int>(
                new List<int>()).Count == 0,
            "Can't convert an empty iterable to an empty list");
        }

        [Fact]
        public void LowBoundTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => 
                new ListOf<int>(
                    new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })
                    [-1]);
        }

        [Fact]
        public void HighBoundTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                new ListOf<int>(
                    new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })
                        [11]);
        }

        [Fact]
        public void SensesChangesInIterable()
        {
            int size = 2;

            var list =
                new ListOf<int>(
                    new Enumerable.Repeated<int>(
                        new ScalarOf<int>(() => 0),
                        new ScalarOf<int>(() =>
                        {
                            Interlocked.Increment(ref size);
                            return size;
                        })));

            Assert.NotEqual(list.Count, list.Count);
        }

    }
}
