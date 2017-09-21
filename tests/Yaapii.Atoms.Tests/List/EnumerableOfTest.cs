using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class EnumerableOfTest
    {
        [Fact]
        public void ConvertsScalarsToEnumerable()
        {
            Assert.True(
                new LengthOf<string>(
                    new EnumerableOf<string>(
                        "a", "b", "c"
                    )
                ).Value() == 3,
                "Can't convert scalars to iterable");
        }

        [Fact]
        public void ConvertsObjectsToEnumerable()
        {
            Assert.True(
                new LengthOf<IText>(
                    new EnumerableOf<IText>(
                        new TextOf("a"), new TextOf("b"), new TextOf("c")
                    )
                ).Value() == 3,
            "Can't convert objects to enumerable");
        }
    }

}
