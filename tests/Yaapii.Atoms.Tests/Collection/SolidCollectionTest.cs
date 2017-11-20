using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Collection.Tests
{
    public sealed class SolidCollectionTest
    {

        [Fact]
        public void BehavesAsCollection()
        {
            Assert.Contains(
                -1,
                new SolidCollection<int>(1, 2, 0, -1));
        }

        [Fact]
        public void MakesListFromMappedIterable()
        {
            var list = new SolidCollection<int>(
                new List.Mapped<int, int>(
                    i => i + 1,
                    new Enumerable.EnumerableOf<int>(1, -1, 0, 1)));

            Assert.True(list.Count == 4, "Can't turn a mapped iterable into a list");
            Assert.True(list.Count == 4, "Can't turn a mapped iterable into a list, again");
        }

    }
}
