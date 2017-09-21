using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class JoinedTest
    {
        [Fact]
        public void TransformsList()
        {
            Assert.True(
                new LengthOf<string>(
                    new Joined<string>(
                        new EnumerableOf<string>("hello", "world", "друг"),
                        new EnumerableOf<string>("how", "are", "you"),
                        new EnumerableOf<string>("what's", "up")
                    )
                ).Value() == 8,
            "Can't concatenate enumerables together");
        }

        [Fact]
        public void JoinsEnumerables()
        {
            Assert.True(
                new LengthOf<IEnumerable<string>>(
                new Joined<IEnumerable<string>>(
                    new Mapped<string, IEnumerable<string>>(
                        new EnumerableOf<string>("x"),
                        str => new EnumerableOf<string>(str)
                    )
                )).Value() == 1,
            "cannot join mapped iterables together");
        }
    }
}
