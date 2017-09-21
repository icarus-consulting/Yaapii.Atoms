using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class CycledEnumeratorTest
    {
        [Fact]
        public void RepeatIteratorTest()
        {
            string expected = "two";

            Assert.True(
                new ItemAtEnumerator<string>(
                    new CycledEnumerator<string>(
                        new EnumerableOf<string>(
                            "one", expected, "three"
                            )
                        ),
                    7).Value() == expected,
                "Can't repeat enumerator");
        }

        [Fact]
        public void EmptyThrowsExceptionTest()
        {
            Assert.False(
                    new CycledEnumerator<string>(
                        new EnumerableOf<string>(
                            new string[0]
                            )
                        ).MoveNext(),
                "Can move but am expected to not move");
        }
    }
}
