using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Collection.Tests
{
    public sealed class MappedTest
    {

        [Fact]
        public void BehavesAsCollection()
        {
            Assert.Contains(
                0,
                new Mapped<int, int>(
                    i => i + 1,
                    new Enumerable.EnumerableOf<int>(-1, 1, 2)
                ));
        }

        [Fact]
        public void TransformsList()
        {

            Assert.Contains(
                new TextOf("HELLO"),
                new Mapped<String, IText>(
                    input => new UpperText(new TextOf(input)),
                    new Enumerable.EnumerableOf<string>("hello", "world", "друг")
                ));
        }

        [Fact]
        public void TransformsEmptyList()
        {
            Assert.Empty(
                new Mapped<String, IText>(
                    input => new UpperText(new TextOf(input)),
                    new List<string>()
                ));
        }

    }
}
