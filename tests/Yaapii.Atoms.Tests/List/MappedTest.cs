using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class MappedTest
    {
        [Fact]
        public void TransformsList()
        {
            Assert.True(
                new ItemAt<IText>(
                    new Mapped<String, IText>(
                        new EnumerableOf<string>("hello", "world", "damn"),
                        input => new UpperText(new TextOf(input))
                        ),
                    0
                ).Value().AsString() == "HELLO",
            "Can't transform an enumerable");
        }

        [Fact]
        public void TransformsEmptyList()
        {
            Assert.True(
                new LengthOf<IText>(
                    new Mapped<String, IText>(
                        new EnumerableOf<string>(),
                        input => new UpperText(new TextOf(input))
                    )).Value() == 0,
                "Can't transform an empty iterable");
        }
    }
}
