using System;
using System.Collections.Generic;
using Xunit;
using Yaapii.Atoms.Collection;
using Yaapii.Atoms.Fail;

namespace Yaapii.Atoms.Collection.Tests
{
    public sealed class JoinedTest
    {
        [Fact]
        public void BehavesAsCollection()
        {
            new Joined<int>(
                new Enumerable.EnumerableOf<int>(1, -1, 2, 0),
                new Enumerable.EnumerableOf<int>(1, -1, 2, 0),
                new Enumerable.EnumerableOf<int>(1, -1, 2, 0)
            );
        }

        [Fact]
        public void Size()
        {
            Assert.True(
                new Joined<String>(
                    new Enumerable.EnumerableOf<string>("hello", "world", "друг"),
                    new Enumerable.EnumerableOf<string>("how", "are", "you"),
                    new Enumerable.EnumerableOf<string>("what's", "up")
                ).Count == 8);
        }

        [Fact]
        public void SizeEmptyReturnZero()
        {
            Assert.True(
                new Joined<String>(
                    new List<string>()
                ).Count == 0);
        }

        [Fact]
        public void WithItemsNotEmpty()
        {
            Assert.NotEmpty(
                new Joined<String>(
                    new Enumerable.EnumerableOf<string>("1", "2"),
                    new Enumerable.EnumerableOf<string>("3", "4")
                ));
        }

        [Fact]
        public void WithoutItemsIsEmpty()
        {
            Assert.Empty(
                new Joined<String>(
                    new List<string>()));
        }

        [Fact]
        public void RejectsAdd()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
             new Joined<int>(
                 new Enumerable.EnumerableOf<int>(1, 2),
                 new Enumerable.EnumerableOf<int>(3, 4),
                 new Enumerable.EnumerableOf<int>(5, 6)
             ).Add(7));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new Joined<String>(
                    new Enumerable.EnumerableOf<string>("w", "a"),
                    new Enumerable.EnumerableOf<string>("b", "c")
                ).Remove("t"));
        }

        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new Joined<int>(
                    new Enumerable.EnumerableOf<int>(10),
                    new Enumerable.EnumerableOf<int>(20)
                ).Clear());
        }
    }
}