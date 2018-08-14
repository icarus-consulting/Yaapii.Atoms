using System.Collections.Generic;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Collection.Tests
{

    public sealed class FilteredTest
    {

        [Fact]
        public void BehavesAsCollection()
        {
            var col =
                new Filtered<int>(i => i < 2, 1, 2, 0, -1);
            Assert.True(col.Contains(1) && col.Contains(-1));
        }

        [Fact]
        public void FiltersList()
        {
            Assert.True(
                new LengthOf(
                    new Filtered<string>(
                        input => input.Length > 4,
                        new EnumerableOf<string>("hello", "world", "друг"))
                ).Value() == 2,
                "cannot filter list"
            );
        }

        [Fact]
        public void FiltersEmptyList()
        {
            var col =
                new Filtered<string>(
                    input => input.Length > 4,
                    new List<string>()
                );
            Assert.Empty(col);
        }

        [Fact]
        public void Size()
        {
            Assert.True(
                new Filtered<string>(
                    input => input.Length >= 4,
                    new EnumerableOf<string>("some", "text", "yes")
                ).Count == 2
            );
        }

        [Fact]
        public void WithItemsNotEmpty()
        {
            Assert.NotEmpty(
                new Filtered<string>(
                    input => input.Length > 4,
                    new EnumerableOf<string>("first", "second")
                )
            );
        }

        [Fact]
        public void WithoutItemsIsEmpty()
        {
            Assert.Empty(
                new Filtered<string>(
                    input => input.Length > 16,
                    new EnumerableOf<string>("third", "fourth")
                )
            );
        }
    }
}