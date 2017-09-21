using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using Yaapii.Atoms;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class StickyEnumeratorTest
    {
        [Fact]
        public void IgnoresChangesInIterable()
        {
            int count = 10;
            Assert.True(
                new JoinedText(
                    ", ",
                    new EnumerableOf<IText>(
                        new MappedEnumerator<int, IText>(
                            new StickyEnumerator<int>(
                                new LimitedEnumerator<int>(
                                    new EndlessEnumerator<int>(Interlocked.Increment(ref count)),
                                    20
                                )
                            ),
                            (number) => new TextOf(number + ""))))
                            .AsString() == "11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11",
                "cannot cache iterator");

        }
    }
}
