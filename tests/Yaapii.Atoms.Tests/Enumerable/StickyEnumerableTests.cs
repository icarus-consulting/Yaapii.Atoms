using System.Threading;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable.Tests
{

    public class StickyEnumerableTests
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
                new LengthOf(list).Value() == new LengthOf(list).Value(),
                "can't ignore changes of underlying iterable");
        }
    }
}
