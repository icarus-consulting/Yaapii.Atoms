using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Collection.Tests
{
    public sealed class ReversedTest
    {

        [Fact]
        public void BehavesAsCollection()
        {
            Assert.True(
                new ItemAt<int>(
                    new Reversed<int>(
                        new Enumerable.EnumerableOf<int>(0, -1, 2))
                ).Value() == 2,
            "cannot behave as a collection");
        }

        [Fact]
        public void ReversesList()
        {
            String last = "last";
            Assert.True(
                new ItemAt<string>(
                    new Reversed<string>(
                        new Enumerable.EnumerableOf<string>(
                            "item", last)
                    )).Value() == last);
        }

        [Fact]
        public void ReversesEmptyList()
        {
            Assert.Empty(
                new Reversed<string>(
                    new List<string>()));
        }

        [Fact]
        public void Size()
        {
            Assert.True(
                new Reversed<string>(
                    new Enumerable.EnumerableOf<string>(
                        "0", "1", "2")
                ).Count == 3);
        }

        [Fact]
        public void NotEmpty()
        {
            Assert.NotEmpty(
                new Reversed<int>(
                    new Enumerable.EnumerableOf<int>(
                        6, 16
                    )
                ));
        }

        [Fact]
        public void Contains()
        {
            String word = "objects";

            Assert.Contains(
                word,
                new Reversed<string>(
                    new Enumerable.EnumerableOf<string>(
                        "hello", "elegant", word)
                ));
        }

        [Fact]
        public void RejectsAdd()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
              new Reversed<int>(
                  new Enumerable.EnumerableOf<int>(
                      1, 2, 3, 4)
              ).Add(6));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new Reversed<int>(
                    new Enumerable.EnumerableOf<int>(
                        1, 2, 3, 4
                    )
                ).Remove(1));
        }



        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new Reversed<int>(
                    new Enumerable.EnumerableOf<int>(
                        1, 2, 3, 4)
                ).Clear());
        }
    }
}
