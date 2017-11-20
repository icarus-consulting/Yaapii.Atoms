using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Fail;

namespace Yaapii.Atoms.Collection.Tests
{
    public sealed class LimitedTest
    {

        [Fact]
        public void BehavesAsCollection()
        {
            Assert.Contains(
                new Limited<int>(
                    2,
                    new Enumerable.EnumerableOf<int>(1, -1, 2, 0)
                ),
                predicate => predicate.Equals(-1));
        }

        [Fact]
        public void Size()
        {
            Assert.True(
                new Limited<string>(
                    2,
                    new Enumerable.EnumerableOf<string>(
                        "hello", "world", "друг")
                ).Count == 2);
        }

        [Fact]
        public void SizeEmptyReturnZero()
        {
            Assert.True(
                new Limited<int>(
                    2,
                    new List<int>()
                ).Count == 0);
        }

        [Fact]
        public void SizeLimitZeroReturnZero()
        {
            Assert.True(
                new Limited<string>(
                    0,
                    new Enumerable.EnumerableOf<string>("1", "2", "3")
                ).Count == 0);
        }

        [Fact]
        public void WithItemsNotEmpty()
        {
            Assert.NotEmpty(
                new Limited<String>(
                    2,
                    new Enumerable.EnumerableOf<string>("first", "second")
                ));
        }

        [Fact]
        public void WithoutItemsIsEmpty()
        {
            Assert.Empty(
                new Limited<String>(
                    0, new Enumerable.EnumerableOf<string>("third", "fourth")
                ));
        }

        [Fact]
        public void RejectsAdd()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
            new Limited<int>(
                2,
                new Enumerable.EnumerableOf<int>(1, 2, 3, 4)
            ).Add(6));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
               new Limited<int>(
                   2,
                   new Enumerable.EnumerableOf<int>(1, 2, 3, 4)
               ).Remove(1));
        }

        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new Limited<int>(
                    2, new Enumerable.EnumerableOf<int>(1, 2, 3, 4)
                ).Clear());
        }
    }
}