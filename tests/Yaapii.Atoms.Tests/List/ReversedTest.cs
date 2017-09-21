using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.List
{
    class ReversedTest
    {
        [Fact]
        public void ReversesIterable()
        {
            Assert.True(
                new JoinedText(
                    " ",
                    new Reversed<string>(
                        new EnumerableOf<string>(
                            "hello", "world", "dude"
                        )
                    )
                ).AsString() == "dude world hello");
        }
    }
}
