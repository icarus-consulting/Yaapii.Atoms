using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using Yaapii.Atoms.Collection;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Collection
{
    public sealed class StickyCollectionTest
    {
        [Fact]
        public void BehavesAsCollection()
        {
            Assert.Contains(
                -1,
                new StickyCollection<int>(
                    new ListOf<int>(1, 2, 0, -1)));
        }

        [Fact]
        public void IgnoresChangesInIterable()
        {
            int size = 2;
            var list =
                new StickyCollection<int>(
                    new ListOf<int>(
                        new Enumerable.Repeated<int>(
                            new ScalarOf<int>(() => 0),
                            new ScalarOf<int>(() =>
                            {
                                Interlocked.Increment(ref size);
                                return size;
                            })
                        )));

        }

        [Fact]
        public void DecoratesArray()
        {
            Assert.True(
                new StickyCollection<int>(-1, 0).Count == 2);
        }

        [Fact]
        public void Empty()
        {
            Assert.Empty(
                new StickyCollection<int>());
        }

        [Fact]
        public void Contains()
        {
            Assert.Contains(
                2,
                new StickyCollection<int>(1, 2));
        }

        [Fact]
        public void RejectsAdd()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new StickyCollection<int>(1, 2).Add(1));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new StickyCollection<int>(1, 2).Remove(1));
        }


        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new StickyCollection<int>(1, 2).Clear());
        }

    }
}
