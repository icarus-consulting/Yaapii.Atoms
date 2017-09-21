using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class RepeatedTest
    {
        [Fact]
        public void AllSameTest()
        {
            int size = 42;
            int element = 11;

            Assert.True(
                new LengthOf<int>(
                        new Filtered<int>(
                            new Repeated<int>(
                                element,
                                size
                            ),
                        input => input == element
                    )
                ).Value() == size,
            "Can't generate an iterable with fixed size");
        }

        [Fact]
        public void EmptyTest()
        {
            Assert.True(
                new LengthOf<int>(
                    new Repeated<int>(0, 0)
                ).Value() == 0,
            "Can't generate an empty iterable");
        }
    }
}
