using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Fail;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class SkippedEnumeratorTest
    {
        [Fact]
        public void SkipEnumerator()
        {
            var skipped =
                new List<string>(
                    new EnumerableOf<string>(
                        new SkippedEnumerator<string>(
                            new EnumerableOf<string>(
                                "one", "two", "three", "four"
                            ).GetEnumerator(),
                        2)));

            Assert.True(new LengthOfEnumerator<string>(skipped.GetEnumerator()).Value() == 2, "cannot skip elements");
            Assert.False(skipped.Contains("one"), "cannot skip elements");
            Assert.False(skipped.Contains("two"), "cannot skip elements");
            Assert.True(skipped.Contains("three"), "cannot skip elements");
            Assert.True(skipped.Contains("four"), "cannot skip elements");
        }

        [Fact]
        public void CannotSkippedMoreThanExists()
        {
            Assert.False(
                new SkippedEnumerator<string>(
                    new EnumerableOf<string>(
                        "one", "two"
                    ).GetEnumerator(),
                    2
                ).MoveNext(),
                "enumerates more elements than exist");
        }
    }
}
