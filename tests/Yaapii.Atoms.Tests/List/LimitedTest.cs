using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Misc;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class LimitedTest
    {
        [Fact]
        public void EnumeratesOverPrefixOfGivenLength()
        {
            Assert.True(
                new SumOfInts(
                new Limited<int>(
                    new EnumerableOf<int>(0, 1, 2, 3, 4),
                    3
                )).Value() == 3,
            "Can't limit an enumerable with more items");
        }

        [Fact]
        public void IteratesOverWholeEnumerableIfThereAreNotEnoughItems()
        {
            Assert.True(
                new SumOfInts(
                new Limited<int>(
                    new EnumerableOf<int>(0, 1, 2, 3, 4, 5),
                    10
                )).Value() == 15,
            "Can't limit an enumerable with less items");
        }

        [Fact]
        public void LimitOfZeroProducesEmptyEnumerable()
        {
            Assert.True(
                new LengthOf<int>(
                    new Limited<int>(
                        new EnumerableOf<int>(0, 1, 2, 3, 4),
                    0
                )).Value() == 0,
            "Can't limit an iterable to zero items");
        }

        [Fact]
        public void NegativeLimitProducesEmptyEnumerable()
        {
            Assert.True(
                new LengthOf<int>(
                new Limited<int>(
                    new EnumerableOf<int>(0, 1, 2, 3, 4),
                    -1
                )).Value() == 0,
                "Can't limit an iterable to negative number of items");
        }

        [Fact]
        public void EmptyEnumerableProducesEmptyEnumerable()
        {
            Assert.True(
                new LengthOf<Nothing>(
                    new Limited<Nothing>(
                        new EnumerableOf<Nothing>(),
                        10
                    )).Value() == 0,
            "Can't limit an empty enumerable");
        }
    }
}
