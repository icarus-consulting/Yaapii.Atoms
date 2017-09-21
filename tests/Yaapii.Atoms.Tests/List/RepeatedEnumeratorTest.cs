using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class RepeatedEnumeratorTest
    {
        [Fact]
        public void AllSameTest()
        {
            int size = 42;
            int element = 11;
            Assert.True(
                new LengthOfEnumerator<int>(
                    new RepeatedEnumerator<int>(
                        element,
                        size
                    )
                ).Value() == size,
                "Can't generate an enumerator with fixed size"
            );
        }

        [Fact]
        public void EmptyTest()
        {
            Assert.True(
                new LengthOfEnumerator<int>(
                    new RepeatedEnumerator<int>(0, 0)
                    ).Value() == 0,
                    "Can't generate an empty enumerator");
        }
    }
}
