using System;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Tests.Enumerable
{
    public sealed class TernaryTest
    {
        [Fact]
        public void EnumeratesLeftWhenMatching()
        {
            Assert.Equal(
                "1 a",
                string.Join(" ",
                    new Ternary<string>(
                        new ManyOf("1", "a"),
                        new ManyOf("2", "b"),
                        ()=> true
                    )
                )
            );
        }

        [Fact]
        public void EnumeratesRightWhenNotMatching()
        {
            Assert.Equal(
                "2 b",
                string.Join(" ",
                    new Ternary<string>(
                        new ManyOf("1", "a"),
                        new ManyOf("2", "b"),
                        () => false
                    )
                )
            );
        }
    }
}

