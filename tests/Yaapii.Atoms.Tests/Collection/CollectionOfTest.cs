using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Collection;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Collection.Tests
{
    public sealed class CollectionOfTest
    {
        [Fact]
        public void BehavesAsCollection()
        {
            var col = new CollectionOf<int>(1, 2, 0, -1);

            Assert.True(col.Contains(1) && col.Contains(2) && col.Contains(0) && col.Contains(-1));
        }

        [Fact]
        public void BuildsCollection()
        {
            Assert.True(
                new CollectionOf<int>(1, 2, 0, -1).Contains(-1),
                "cannot build a collection");
        }

        [Fact]
        public void BuildsCollectionFromIterator()
        {
            Assert.True(
                new CollectionOf<int>(
                    new ListOf<int>(1, 2, 0, -1).GetEnumerator()).Contains(-1),
            "cannot build collection from enumerator");
        }

    }
}