using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class StickyEnumerableTest
    {
        [Fact]
        public void IgnoresChangesInIterable()
        {
            int size = 2;
            var list =
                new StickyEnumerable<int>(
                    new Limited<int>(
                        new Endless<int>(1),
                        new ScalarOf<int>(() => Interlocked.Increment(ref size))
                        ));

            Assert.True(
                new LengthOf<int>(list).Value() == new LengthOf<int>(list).Value(),
                "can't ignore changes of underlying iterable");
        }
    }
}